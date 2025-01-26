using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProtoToolkit.Scripts.Audio
{
    public class SfxComponent : MonoBehaviour
    {
        [SerializeField] private bool _autoPlay = false;
        [FormerlySerializedAs("_audioClip")] [SerializeField] private List<AudioClip> clips = new List<AudioClip>();
        [SerializeField] private float _minPitch = 1f;
        [SerializeField] private float _maxPitch = 1f;

        public void OnEnable()
        {
            if (!_autoPlay) return;
            Play();
        }
    
        public void Play()
        {
            if (clips.Count == 0) return;
            var clip = clips[Random.Range(0, clips.Count - 1)];
            AudioManager.Instance.PlaySound(this, clip, false, Random.Range(_minPitch, _maxPitch));
        }
        
    }
}
