using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.DIContainer.Scripts
{
    public readonly struct BindContainer<T>
    {
        private readonly T _value;
        private readonly DIContainer _diContainer;
        
        public HashSet<Type> Types { get; }
        
        public BindContainer(T value, DIContainer diContainer)
        {
            _value = value;
            Types = new HashSet<Type>();
            _diContainer = diContainer;
            
            RegisterInterface<IInitializable>();
            RegisterInterface<IUpdatable>();
            RegisterInterface<ILateUpdatable>();
            RegisterInterface<IDisposable>();
        }

        private void RegisterInterface<TType>() where TType : class
        {
            var interfaceType = typeof(TType);
            if (_value is TType)
            {
                Types.Add(interfaceType);
            }
        }
        
        public BindContainer<T> AsType<TType>() where TType : class
        {
            Type type = typeof(TType);
            if (_value is TType)
            {
                Types.Add(type);
            }
            else
            {
                Debug.LogError($"Type {type} is not assignable from {typeof(T)}");
            }
            
            return this;
        }

        public T Register()
        {
            _diContainer.RegisterBindContainer(this);
            return this;
        }
        
        public static implicit operator T(BindContainer<T> container)
        {
            return container._value;
        }
    }
}