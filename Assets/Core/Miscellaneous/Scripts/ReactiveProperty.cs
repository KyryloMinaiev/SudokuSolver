using System;

namespace Content.Features.UIModule.Scripts
{
    public class ReactiveProperty<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (Equals(_value, value))
                {
                    return;
                }
                
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }

        public event Action<T> OnValueChanged;

        public ReactiveProperty(T initialValue =  default)
        {
            _value = initialValue;
        }

        public void Subscribe(Action<T> action)
        {
            OnValueChanged += action;
            action?.Invoke(_value);
        }

        public void Unsubscribe(Action<T> action)
        {
            OnValueChanged -= action;
        }
    }
}