using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProtoToolkit.Scripts.UI.Common
{
    public class ListButton : MonoBehaviour, ISelectable, IUIDataProvider
    {
        [Header("Components")]
        [SerializeField]
        private Button _button;
        
        [Header("Events")]
        public UnityEvent<int> OnClick;
        public UnityEvent<IUIData> OnDataChanged = new UnityEvent<IUIData>();
        public UnityEvent<bool> OnSelectedChanged;

        public IUIData Data { get; private set; }

        public void OnValidate()
        {
            _button = GetComponent<Button>();
        }

        public void Awake()
        {
            _button?.onClick.AddListener(OnButtonPressed);
        }

        public void OnEnable()
        {
            OnSelectedChanged.Invoke(IsSelected);
            OnDataChanged.Invoke(Data);
        }

        public void SetData(IUIData data)
        {
            if (Data == data)
                return;
            Data = data;
            OnDataChanged.Invoke(Data);
        }

        public void OnButtonPressed()
        {
            OnClick.Invoke(transform.GetSiblingIndex());
        }

        public bool IsSelected { get; private set; }
        
        public void SetSelected(bool value)
        {
            if (IsSelected == value)
                return;
            IsSelected = value;
            OnSelectedChanged.Invoke(value);
        }

        public UnityEvent<IUIData> DataChangedEvent => OnDataChanged;

        public IUIData GetUIData()
        {
            return Data;
        }
    }
}
