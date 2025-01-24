using UnityEngine;
using UnityEngine.UI;

namespace ProtoToolkit.Scripts.UI.Common
{
    [RequireComponent(typeof(LayoutElement))]
    public class SelectionSizeToggler : MonoBehaviour
    {
        [SerializeField] private LayoutElement _element;
        [SerializeField] private Vector2 _defaultSize;
        [SerializeField] private Vector2 _selectionSize;
        [SerializeField] private AnimationCurve _curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        [SerializeField] private float _animationDuration = 0.33f;
        private float _targetSize;
        private float _currentSize;

        public void OnValidate()
        {
            _element = GetComponent<LayoutElement>();
            
            _currentSize = 0;
            _element.preferredWidth = _defaultSize.x;
            _element.preferredHeight = _defaultSize.y;
        }
        
        public void OnSelectionChanged(bool value)
        {
            _targetSize = value ? 1 : 0;
        }

        public void Update()
        {
            if (Mathf.Approximately(_currentSize, _targetSize))
                return;

            if (_currentSize < _targetSize)
            {
                _currentSize += Time.deltaTime * 1 / _animationDuration;
            }
            else
            {
                _currentSize -= Time.deltaTime * 1 / _animationDuration;
            }
            _currentSize = Mathf.Clamp01(_currentSize);
            var evaluate = _curve.Evaluate(_currentSize);
            var newSize = Vector2.Lerp(_defaultSize, _selectionSize, evaluate);
            _element.preferredWidth = newSize.x;
            _element.preferredHeight = newSize.y;
        }
    }
}
