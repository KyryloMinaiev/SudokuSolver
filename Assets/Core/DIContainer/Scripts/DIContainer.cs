using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.DIContainer.Scripts
{
    public class DIContainer : MonoBehaviour
    {
        private HashSet<Type> _defaultTypes = new HashSet<Type>
            { typeof(IInitializable), typeof(IDisposable), typeof(IUpdatable), typeof(ILateUpdatable) };

        private readonly HashSet<IInitializable> _initializables = new HashSet<IInitializable>();
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

            FillParameters(_tempParametersList, parameters, type);
            T instance = Activator.CreateInstance(type, _tempParametersList.ToArray()) as T;
            return new BindContainer<T>(instance, this, _defaultTypes);
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
            RegisterInterface<T, IInitializable>(bindContainer, bindContainer.RegisteredDefaultTypes, _initializables);
            RegisterInterface<T, IUpdatable>(bindContainer, bindContainer.RegisteredDefaultTypes, _updatables);
            RegisterInterface<T, ILateUpdatable>(bindContainer, bindContainer.RegisteredDefaultTypes, _lateUpdatables);
            RegisterInterface<T, IDisposable>(bindContainer, bindContainer.RegisteredDefaultTypes, _disposables);
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

        private void RegisterInterface<TContainer, TInterface>(TContainer obj, HashSet<Type> registeredContainerTypes,
            HashSet<TInterface> interfaceSet) where TInterface : class
        {
            Type interfaceType = typeof(TInterface);
            if (registeredContainerTypes.Contains(interfaceType))
            {
                interfaceSet.Add(obj as TInterface);
            }
        }

        private void Awake()
        {
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(gameObject);
            RegisterSelf();
        }

        private void RegisterSelf()
        {
            _savedTypes.Add(typeof(DIContainer), this);
        }

        private void Update()
        {
            foreach (var initializable in _initializables)
            {
                initializable.Initialize();
            }
            
            _initializables.Clear();
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

        public GameObject InstantiatePrefab(GameObject prefab)
        {
            return InstantiatePrefabInternal(prefab, null);
        }

        public GameObject InstantiatePrefab(GameObject prefab, Transform parent)
        {
            return InstantiatePrefabInternal(prefab, parent);
        }

        public TComponent InstantiateComponent<TComponent>(Object prefab) where TComponent : MonoBehaviour
        {
            GameObject obj = InstantiatePrefab(GetGameObject(prefab));
            return obj.GetComponent<TComponent>();
        }
        
        public TComponent InstantiateComponent<TComponent>(Object prefab, Transform parent) where TComponent : MonoBehaviour
        {
            GameObject obj = InstantiatePrefab(GetGameObject(prefab), parent);
            return obj.GetComponent<TComponent>();
        }

        private GameObject GetGameObject(Object prefab)
        {
            if (prefab is GameObject gameObject)
            {
                return gameObject;
            }
            
            return ((Component)prefab).gameObject;
        }

        private GameObject InstantiatePrefabInternal(GameObject prefab, Transform parent)
        {
            bool prefabIsActive = prefab.activeSelf;
            prefab.SetActive(false);

            GameObject instance = Instantiate(prefab, parent);
            Inject(instance);

            if (prefabIsActive)
            {
                prefab.SetActive(true);
                instance.SetActive(true);
            }
            
            return instance;
        }

        private void Inject(GameObject obj)
        {
            var objectComponents = obj.GetComponents<MonoBehaviour>();
            foreach (var component in objectComponents)
            {
                InjectIntoComponent(component);
            }
            
            var childComponents = obj.GetComponentsInChildren<MonoBehaviour>();
            foreach (var component in childComponents)
            {
                InjectIntoComponent(component);
            }
        }

        private void FillParameters(List<object> parameters, ParameterInfo[] methodParameters, Type type)
        {
            parameters.Clear();
            foreach (var parameter in methodParameters)
            {
                if (_savedTypes.TryGetValue(parameter.ParameterType, out var value))
                {
                    parameters.Add(value);
                }
                else
                {
                    throw new Exception($"Cannot resolve dependency {parameter.ParameterType.Name} for {type.Name}");
                }
            }
        }

        private void InjectIntoComponent(MonoBehaviour component)
        {
            Type componentType = component.GetType();
            var methods = componentType
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => m.GetCustomAttribute<InjectAttribute>() != null);

            foreach (var methodInfo in methods)
            {
                var parameters = methodInfo.GetParameters();
                FillParameters(_tempParametersList, parameters, componentType);
                methodInfo.Invoke(component, _tempParametersList.ToArray());
            }
        }
    }
}