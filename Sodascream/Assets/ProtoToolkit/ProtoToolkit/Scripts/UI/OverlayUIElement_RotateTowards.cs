using UnityEngine;
using UnityEngine.UI;

namespace ProtoToolkit.Scripts.UI
{
    public class OverlayUIElementRotateTowards : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private OverlayUIElement _element;
        [SerializeField] private Graphic[] _graphics;
        private float _previousRotation;

        public void OnValidate()
        {
            _rectTransform = GetComponent<RectTransform>();
            if (_element == null)
            {
                _element = GetComponentInParent<OverlayUIElement>();
            }
            _graphics = GetComponentsInChildren<Graphic>(true);
        }
        
        public void Update()
        {
            var show = _element.DistanceToTarget > 2f;

            foreach (var graphic in _graphics)
            {
                if (graphic.enabled != show)
                {
                    graphic.enabled = show;
                }
            }
            
            if (!show || Mathf.Approximately(_previousRotation, _element.Rotation)) return;
            _rectTransform.eulerAngles = new Vector3(0, 0, _element.Rotation);
            _previousRotation = _element.Rotation;
        }
    }
}
