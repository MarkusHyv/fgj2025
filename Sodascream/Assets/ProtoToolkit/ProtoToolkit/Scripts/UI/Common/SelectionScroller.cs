using UnityEngine;
using UnityEngine.UI;

namespace ProtoToolkit.Scripts.UI.Common
{
    [RequireComponent(typeof(ScrollRect))]
    public class SelectionScroller : MonoBehaviour
    {
        [SerializeField]
        private ScrollRect _scrollView;
        [SerializeField]
        private RectTransform _contentHolder;

        public void OnValidate()
        {
            _scrollView = GetComponent<ScrollRect>();
            _contentHolder = _scrollView.content;
        }
        
        public static void ScrollObjectToView(ScrollRect scrollRect, RectTransform rectTransform)
        {
            if (rectTransform == null)
                return;
            
            var targetPos = rectTransform.anchoredPosition;
            var parentSize = scrollRect.content.sizeDelta;
            var targetNormalizedPosition = targetPos / parentSize;
            scrollRect.verticalNormalizedPosition = 1-Mathf.Abs(targetNormalizedPosition.y);
        }
    }
}
