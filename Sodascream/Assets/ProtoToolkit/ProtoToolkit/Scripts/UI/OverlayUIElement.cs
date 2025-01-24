using UnityEngine;

namespace ProtoToolkit.Scripts.UI
{
    public class OverlayUIElement : MonoBehaviour
    {
        [SerializeField] protected Transform _target;

        public virtual Transform GetOverlayTarget() => _target;
        
        public virtual void SetOverlayTarget(Transform target) => _target = target;
    
        [SerializeField] protected bool _keepOnScreenEdges;
        public bool KeepOnScreenEdges => _keepOnScreenEdges;
        [SerializeField] protected RectTransform _rectTransform;
        public RectTransform RectTransform => _rectTransform;
        [HideInInspector] public float Rotation;
        [HideInInspector] public float DistanceToTarget;

        public void OnValidate()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}
