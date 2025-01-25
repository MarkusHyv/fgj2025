using UnityEngine;

namespace ProtoToolkit.Scripts.UI.Common
{
    public abstract class UIDataBinder<T> : MonoBehaviour where T : IUIData
    {
        [SerializeField] private Component _providerComponent;
        private IUIDataProvider _provider;
        
        public void Awake()
        {
            _provider = _providerComponent as IUIDataProvider;
        }
        
        public virtual void OnValidate()
        {
            if (Application.isPlaying)
                return;
            _providerComponent = GetComponentInParent<IUIDataProvider>(true) as Component;
        }
    
        public virtual void OnEnable()
        {
            _provider = _providerComponent as IUIDataProvider;
            _provider?.DataChangedEvent.AddListener(OnDataChanged);
            OnDataChanged(_provider?.GetUIData());
        }

        public virtual void OnDisable()
        {
            _provider?.DataChangedEvent.RemoveListener(OnDataChanged);
        }

        public void OnDataChanged(IUIData data)
        {
            var d = (T)data;
            if (d == null)
                return;
            OnDataChanged(d);
        }

        public virtual void OnDataChanged(T data)
        {
            ApplyData(data);
        }

        public abstract void ApplyData(T data);
    }
}
