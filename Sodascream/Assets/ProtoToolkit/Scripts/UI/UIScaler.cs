using ProtoToolkit.Scripts.UI.Graphics;
using UnityEngine;
using UnityEngine.UI;

namespace ProtoToolkit.Scripts.UI
{
    [RequireComponent(typeof(CanvasScaler))]
    public class UIScaler : MonoBehaviour
    {
        [SerializeField]
        private CanvasScaler _canvasScaler;
        [SerializeField]
        private Vector2Int _currentResolution;

        private float _timer;

        public void OnValidate()
        {
            _canvasScaler = GetComponent<CanvasScaler>();
        }
    
        public void OnEnable()
        {
            GraphicsManager.ScreenResolutionChanged.AddListener(ResolutionChanged);
            ResolutionChanged(GraphicsManager.CurrentResolution);
        }

        public void OnDisable()
        {
            GraphicsManager.ScreenResolutionChanged.RemoveListener(ResolutionChanged);
        }

        public void ResolutionChanged(Vector2Int resolution)
        {
            _currentResolution = resolution;
            SetScaling(_currentResolution);
        }

        public void SetScaling(Vector2Int resolution)
        {
            var ratio = (float)resolution.x / (float)resolution.y;
            var referenceRatio = _canvasScaler.referenceResolution.x / _canvasScaler.referenceResolution.y;
            _canvasScaler.matchWidthOrHeight = ratio >= referenceRatio ? 1f : 0f;
        }
    }
}
