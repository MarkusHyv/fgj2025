using UnityEngine;

namespace ProtoToolkit.Scripts.UI
{
    public static class UIHelpers
    {
        public static void GameObjectSetActive(this GameObject obj, bool active)
        {
            if (obj.activeSelf == active) return;
            obj.SetActive(active);
        }

        public static void ClearChildren(this Transform t)
        {
            var childCount = t.childCount;
            for (var i = childCount - 1; i >= 0; i--)
            {
                Object.Destroy(t.GetChild(i).gameObject);
            }
        }

        public static void SetCanvasGroupAlpha(this CanvasGroup canvasGroup, float value)
        {
            if (Mathf.Approximately(canvasGroup.alpha, value))
                return;

            canvasGroup.alpha = value;
        }
    }
}
