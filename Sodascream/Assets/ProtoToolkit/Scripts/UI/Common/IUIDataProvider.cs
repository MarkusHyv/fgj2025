using UnityEngine.Events;

namespace ProtoToolkit.Scripts.UI.Common
{
    public interface IUIDataProvider
    {
        public UnityEvent<IUIData> DataChangedEvent { get; }
        public IUIData GetUIData();
    }
}
