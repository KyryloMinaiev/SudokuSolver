using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.DIContainer.Scripts
{
    public readonly struct BindContainer<T>
    {
        private readonly T _value;
        private readonly DIContainer _diContainer;
        
        public HashSet<Type> RegisteredDefaultTypes { get; }
        public HashSet<Type> Types { get; }
        
        public T Value => _value;
        
        public BindContainer(T value, DIContainer diContainer, HashSet<Type> defaultTypes)
        {
            _value = value;
            RegisteredDefaultTypes = new HashSet<Type>();
            Types = new HashSet<Type>();
            _diContainer = diContainer;

            RegisterDefaultTypes(defaultTypes);
        }

        private void RegisterDefaultTypes(HashSet<Type> defaultTypes)
        {
            foreach (var type in defaultTypes)
            {
                RegisterDefaultType(type);
            }
        }

        private void RegisterDefaultType(Type type)
        {
            if (type.IsAssignableFrom(typeof(T)))
            {
                RegisteredDefaultTypes.Add(type);
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