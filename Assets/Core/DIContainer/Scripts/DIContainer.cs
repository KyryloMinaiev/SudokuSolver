using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Core.DIContainer.Scripts
{
    public class DIContainer : MonoBehaviour
    {
        private readonly HashSet<IUpdatable> _updatables = new HashSet<IUpdatable>();
        private readonly HashSet<ILateUpdatable> _lateUpdatables = new HashSet<ILateUpdatable>();
        private readonly HashSet<IDisposable> _disposables = new HashSet<IDisposable>();
        private readonly Dictionary<Type, object> _savedTypes = new Dictionary<Type, object>();

        private readonly List<object> _tempParametersList = new List<object>();
        
        public BindContainer<T> Bind<T>() where T : class
        {
            var type = typeof(T);
            var ctor = SelectConstructor(type);
            var parameters = ctor.GetParameters();
            
            _tempParametersList.Clear();
            foreach (var parameter in parameters)
            {
                if (_savedTypes.TryGetValue(parameter.ParameterType, out var value))
                {
                    _tempParametersList.Add(value);   
                }
                else
                {
                    throw new Exception($"Cannot resolve dependency {parameter.ParameterType.Name} for {type.Name}");
                }
            }
            
            T instance = Activator.CreateInstance(type, _tempParametersList.ToArray()) as T;
            return new BindContainer<T>(instance, this);
        }

        public T Get<T>() where T : class
        {
            if (_savedTypes.TryGetValue(typeof(T), out var value))
            {
                return value as T;
            }
            
            return null;
        }

        private ConstructorInfo SelectConstructor(Type type)
        {
            return type.GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length)
                .FirstOrDefault();
        }

        public void RegisterBindContainer<T>(BindContainer<T> bindContainer)
        {
            RegisterInterface(bindContainer.Updatable, _updatables);
            RegisterInterface(bindContainer.LateUpdatable, _lateUpdatables);
            RegisterInterface(bindContainer.Disposable, _disposables);
            RegisterInitializable(bindContainer.Initializable);
            RegisterTypes<T>(bindContainer, bindContainer.Types);
        }

        public void Install<T>() where T : class, IInstaller<T>, new()
        {
            T instance = new T();
            instance.Install(this);
        }

        private void RegisterTypes<T>(T value, HashSet<Type> types)
        {
            foreach (var type in types)
            {
                if (!_savedTypes.TryAdd(type, value))
                {
                    Debug.LogError($"Type {type} already registered");
                }
            }
        }

        private void RegisterInterface<T>(T interfaceObject, HashSet<T> set)
        {
            if (interfaceObject != null)
            {
                set.Add(interfaceObject);
            }
        }

        private void RegisterInitializable(IInitializable initializable)
        {
            if (initializable != null)
            {
                initializable.Initialize();
            }   
        }

        private void Awake()
        {
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(gameObject);
        }
        
        private void Update()
        {
            foreach (var updatable in _updatables)
            {
                updatable.Update();
            }
        }

        private void LateUpdate()
        {
            foreach (var lateUpdatable in _lateUpdatables)
            {
                lateUpdatable.LateUpdate();
            }
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            
            _savedTypes.Clear();
        }
    }
}