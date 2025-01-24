using UnityEngine;

namespace Audio
{
    public class AudioCube : MonoBehaviour
    {
        public float ScaleModifier = 5f;
        public void OnMaxLevelChange(float value)
        {
            var scale = 1 + value * ScaleModifier;
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
