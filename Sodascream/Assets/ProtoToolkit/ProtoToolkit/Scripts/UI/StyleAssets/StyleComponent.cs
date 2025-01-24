using UnityEngine;

namespace ProtoToolkit.Scripts.UI.StyleAssets
{
    public abstract class StyleComponent<T1, T2, T3> : StyleComponentBase where T1 : Component where T2 : StyleAsset<T3>
    {
        [SerializeField]
        private T1 _target;
        [SerializeField]
        private T2 _styleAsset;
        public T2 StyleAsset => _styleAsset;

        public virtual void OnValidate()
        {
            _target = GetComponent<T1>();
            if (_target != null && _styleAsset != null)
            {
                ApplyStyle();
            }
        }
    }
}
