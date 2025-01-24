using UnityEngine;
using UnityEngine.Events;

namespace ProtoToolkit.Scripts.UI.Overlay
{
    public class UIGenericOverlay : MonoBehaviour
    {
        [SerializeField] protected OverlayUIElement _overlayUIElement;
        public OverlayUIElement OverlayUIElement => _overlayUIElement;
        [SerializeField] private CanvasGroup _canvasGroup;
        public UnityEvent<UIGenericOverlay> DisableEvent = new UnityEvent<UIGenericOverlay>();
        protected byte _targetSlot;
        
        public byte GetTargetSlot() => _targetSlot;
        
        public virtual void AssignToTarget(IOverlayTarget target)
        {
            _targetSlot = target.id;
            OverlayUIElement.SetTarget(target.transform);
        }
        
        public void OnDisable()
        {
            DisableEvent.Invoke(this);
        }
    }
}
