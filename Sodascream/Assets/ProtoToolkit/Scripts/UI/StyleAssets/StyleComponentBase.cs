using UnityEngine;

namespace ProtoToolkit.Scripts.UI.StyleAssets
{
    public abstract class StyleComponentBase : MonoBehaviour
    {
        public abstract bool CompareStyle(object style);
        public abstract void ApplyStyle();
    }
}
