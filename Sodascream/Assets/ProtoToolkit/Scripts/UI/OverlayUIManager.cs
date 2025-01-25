using System.Collections.Generic;
using UnityEngine;

namespace ProtoToolkit.Scripts.UI
{
    public class OverlayUIManager : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _inactive;
        [SerializeField] private Camera _camera;
        private List<OverlayUIElement> _elements = new List<OverlayUIElement>();

        [SerializeField] private float _minDistanceFromCenter = 450f;
        [SerializeField] private float _maxDistanceFromCenter = 500f;
        [SerializeField] private float _blendStartDistance;
        [SerializeField] private float _blendDistance = 200f;
        [SerializeField] private Stack<OverlayUIElement> _invalidElements = new Stack<OverlayUIElement>();
    
        public void OnValidate()
        {
            _rectTransform = GetComponent<RectTransform>();
            if (_camera == null)
            {
                _camera = FindFirstObjectByType<Camera>();
            }
        }

        public void RegisterOverlayUIElement(OverlayUIElement element)
        {
            element.transform.SetParent(_rectTransform);
            element.transform.localScale = Vector3.one;
            element.gameObject.GameObjectSetActive(true);
            _elements.Add(element);
            UpdatePosition(element);
        }

        public void UnregisterOverlayUIElement(OverlayUIElement element)
        {
            element.gameObject.GameObjectSetActive(false);
            _elements.Remove(element);
        }

#if UNITY_STANDALONE_ANDROID || UNITY_EDITOR

        public void LateUpdate()
        {
            for (var i = _elements.Count - 1; i >= 0; i--)
            {
                if (i >= _elements.Count)
                    continue;
                UpdatePosition(_elements[i]);
            }
        }
    
#else
    
    public void LateUpdate()
    {
        for (var i = _elements.Count - 1; i > 0; i--)
        {
            if (i >= _elements.Count)
                continue;
            UpdatePosition(_elements[i]);
        }
    }
    
#endif
    
        public void UpdatePosition(OverlayUIElement element)
        {
            var target = element.GetOverlayTarget();
            if (!target)
            {
                element.gameObject.SetActive(false);
                return;
            }

            if (element.gameObject.activeSelf == false)
            {
                element.gameObject.SetActive(true);
            }
        
            Vector2 screenPoint = _camera.WorldToScreenPoint(target.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform,
                screenPoint, null, out var newPos);

            var currentPosition = element.RectTransform.anchoredPosition;
            var targetPos = newPos;
        
            if (element.KeepOnScreenEdges)
            {
                //TODO: cap the position based on safespace.

                var center = _rectTransform.sizeDelta * 0.5f;
                var distance = Vector2.Distance(center, targetPos);
                if (distance > _minDistanceFromCenter)
                {
                    var blend = (distance - _blendStartDistance) / _blendDistance;
                    blend = Mathf.SmoothStep(0, 1, Mathf.Clamp01(blend));
                    var radius = Mathf.Lerp(_minDistanceFromCenter, _maxDistanceFromCenter, blend);
                    targetPos = Vector2.Lerp(center, targetPos, radius / distance);
                }

                var directionVector = center - targetPos;
                directionVector = directionVector.normalized;
                element.Rotation = Vector2.SignedAngle(new Vector2(0, 1), -directionVector);
                element.DistanceToTarget = Vector2.Distance(targetPos, newPos);
            }
        
            if (Vector2.Distance(newPos, currentPosition) > 1f)
            {
                element.RectTransform.anchoredPosition = targetPos;
            }
        
        }
    
    }
}
