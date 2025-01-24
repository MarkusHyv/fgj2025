using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProtoToolkit.Scripts.UI.Overlay
{
    public abstract class UIGenericOverlayManager<T> : MonoBehaviour where T : UIGenericOverlay
    {
        [SerializeField]
        protected OverlayUIManager _overlayUIManager;
        [SerializeField]
        private T _overlayPrefab;

        protected readonly Stack<T> _overlayStack = new Stack<T>();
        protected readonly Dictionary<IOverlayTarget, T> _objectToOverlay = new Dictionary<IOverlayTarget, T>();
        protected readonly Dictionary<T, IOverlayTarget> _overlayToObject = new Dictionary<T, IOverlayTarget>();

        public void OnValidate()
        {
            _overlayUIManager = GetComponent<OverlayUIManager>();
        }
        
        public virtual void OnEnable()
        {
            GetInitialState();
        }

        protected abstract void GetInitialState();
        

        public virtual void OnDisable()
        {
            Reset();
        }

        public virtual void Reset()
        {
            var widgets = _objectToOverlay.Values.ToList();
            foreach (var widget in widgets)
            {
                ReleaseOverlay(widget);
            }
            
            _objectToOverlay.Clear();
            _overlayToObject.Clear();
        }

        protected T GetOverlay(IOverlayTarget target)
        {
            if (_objectToOverlay.TryGetValue(target, out var overlay))
            {
                if (overlay != null)
                {
                    return overlay;   
                }
                _objectToOverlay.Remove(target);
                Debug.LogError("Player has an invalid overlay that has been destroyed!");
            }
            
            if (_overlayStack.Count > 0)
            {
                overlay = _overlayStack.Pop();
            }
            else
            {
                overlay = Instantiate(_overlayPrefab);
                overlay.DisableEvent.AddListener(ReleaseOverlay);
            }
            _objectToOverlay.Add(target, overlay);
            _overlayToObject.Add(overlay, target);
            overlay.AssignToTarget(target);
            _overlayUIManager.RegisterOverlayUIElement(overlay.OverlayUIElement);
            return overlay;
        }

        protected void ReleaseOverlay(UIGenericOverlay overlay)
        {
            ReleaseOverlay(overlay as T);
        }

        protected void ReleaseOverlay(T overlay)
        {
            if (!_overlayToObject.TryGetValue(overlay, out var id))
                return;
            
            _objectToOverlay.Remove(id);
            _overlayToObject.Remove(overlay);
            _overlayUIManager.UnregisterOverlayUIElement(overlay.OverlayUIElement);
            _overlayStack.Push(overlay);
        }
        
    }
}
