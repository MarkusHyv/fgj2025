using System;
using UnityEngine;
using UnityEngine.Events;

namespace ProtoToolkit.Scripts.DTO
{
    public abstract class DtoBase<T> : ScriptableObject
    {
        private T _currentValue;
        public T GetCurrentValue() => _currentValue;
        [HideInInspector]
        public UnityEvent<T> OnValueChanged = new UnityEvent<T>();

        public void BindProperty(ref T property, UnityAction<T> onValueChangedEvent)
        {
            onValueChangedEvent(_currentValue);
            OnValueChanged.AddListener(onValueChangedEvent);
        }
        
        public void UnbindProperty(UnityAction<T> onValueChangedEvent) => OnValueChanged.RemoveListener(onValueChangedEvent);
    }
}
