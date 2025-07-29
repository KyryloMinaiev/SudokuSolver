using System;
using System.Collections.Generic;

namespace Core.DIContainer.Scripts
{
    public struct BindContainer<T>
    {
        private readonly T _value;
        private readonly DIContainer _diContainer;
        
        public IInitializable Initializable { get; private set; }
        public IUpdatable Updatable { get; private set; }
        public ILateUpdatable LateUpdatable { get; private set; }
        public IDisposable Disposable { get; private set; }
        public HashSet<Type> Types { get; private set; }
        
        public BindContainer(T value, DIContainer diContainer)
        {
            _value = value;
            Initializable = null;
            Updatable = null;
            LateUpdatable = null;
            Disposable = null;
            Types = new HashSet<Type>();
            _diContainer = diContainer;
        }
        
        public BindContainer<T> AsInitializable()
        {
            Initializable = _value as IInitializable;
            return this;
        }
        
        public BindContainer<T> AsUpdatable()
        {
            Updatable = _value as IUpdatable;
            return this;
        }

        public BindContainer<T> AsLateUpdatable()
        {
            LateUpdatable = _value as ILateUpdatable;
            return this;
        }

        public BindContainer<T> AsType<TType>()
        {
            Type type = typeof(TType);
            Types.Add(type);
            return this;
        }

        public BindContainer<T> AsDisposable()
        {
            Disposable = _value as IDisposable;
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