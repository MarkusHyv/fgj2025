using UnityEngine;

namespace ProtoToolkit.Scripts.Audio
{
    public class AudioComponent : MonoBehaviour
    {
        [SerializeField] private bool _autoPlay = false;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private bool loop;
        [SerializeField] private float _minPitch = 1f;
        [SerializeField] private float _maxPitch = 1f;

        public void OnEnable()
        {
            if (!_autoPlay) return;
            Play();
        }
    
        public void Play()
        {
            AudioManager.Instance.PlaySound(this, _audioClip, loop, Random.Range(_minPitch, _maxPitch));
        }

        public void Stop()
        {
            AudioManager.Instance.StopSound(this, _audioClip);
        }

        public void OnDisable()
        {
            if (!_autoPlay || !loop) return;
            Stop();
        }
    }
}
