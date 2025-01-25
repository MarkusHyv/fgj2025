using UnityEngine;

namespace ProtoToolkit.Scripts.UI.Overlay
{
    public interface IOverlayTarget
    {
        public byte id { get; }
        public Transform transform { get; }
    }
}
