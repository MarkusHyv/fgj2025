using System;
using UnityEngine;
using UnityEngine.Events;

namespace ProtoToolkit.Scripts.DTO
{
    public class DtoComponentBase<T1, T2> : MonoBehaviour where T1 : DtoBase<T2>
    {
        [SerializeField]
        private T1 _dto;

        [SerializeField] private UnityEvent<T2> OnValueChanged = new UnityEvent<T2>();

        protected void OnEnable()
        {
            if (_dto == null) return;
            T2 val = default;
            _dto.BindProperty(ref val, ValueChangedEvent);
        }

        protected void OnDisable()
        {
            if (_dto == null) return;
            _dto.UnbindProperty(ValueChangedEvent);
        }

        protected void ValueChangedEvent(T2 value)
        {
            OnValueChanged.Invoke(value);
        }
    }
}
