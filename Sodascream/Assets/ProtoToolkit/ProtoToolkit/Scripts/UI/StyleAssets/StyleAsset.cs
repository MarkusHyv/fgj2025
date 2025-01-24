using UnityEngine;

namespace ProtoToolkit.Scripts.UI.StyleAssets
{
    public class StyleAsset<T> : ScriptableObject
    {
        private T _data;
        public T Data => _data;

        public void OnValidate()
        {
            ApplyNewValuesToComponents();
        }

        private void ApplyNewValuesToComponents()
        {
            var components = FindObjectsByType<StyleComponentBase>(FindObjectsSortMode.InstanceID);
            foreach (var component in components)
            {
                if (component.CompareStyle(this))
                {
                    component.ApplyStyle();
                }
            }
        }
    }
}
