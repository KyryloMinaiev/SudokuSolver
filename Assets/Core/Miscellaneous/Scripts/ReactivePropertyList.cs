using System;
using System.Collections.Generic;

namespace Content.Features.UIModule.Scripts
{
    public class ReactivePropertyList<T>
    {
        private readonly List<T> _value;

        public List<T> Value => _value;
        public event Action<List<T>> OnValueChanged;

        public ReactivePropertyList()
        {
            _value = new List<T>();
        }
        
        public int Count => _value.Count;

        public void Add(T item)
        {
            _value.Add(item);
            OnValueChanged?.Invoke(_value);
        }

        public void Remove(T item)
        {
            _value.Remove(item);
            OnValueChanged?.Invoke(_value);
        }
        
        public void Clear()
        {
            _value.Clear();
            OnValueChanged?.Invoke(_value);
        }

        public void RemoveAt(int index)
        {
            _value.RemoveAt(index);
            OnValueChanged?.Invoke(_value);       
        }
        
        public T this[int index] => _value[index];

        public void Subscribe(Action<List<T>> action)
        {
            OnValueChanged += action;
            action?.Invoke(_value);
        }

        public void Unsubscribe(Action<List<T>> action)
        {
            OnValueChanged -= action;
        }
    }
}