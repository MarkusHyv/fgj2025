using UnityEngine;

namespace ProtoToolkit.Scripts.UI.State
{
    public class UIAnimation : MonoBehaviour
    {
        [SerializeField] private Animation _animation;

        public void OnValidate()
        {
            _animation = GetComponent<Animation>();
        }

        public void PlayAnimation(AnimationClip clip)
        {
            _animation.clip = clip;
            _animation.AddClip(clip, clip.name);
            _animation.Play(clip.name);
        }
    }
}
