using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class MicrophoneSystem : MonoBehaviour
    {
        public AudioSource _audioSource;
        public AudioClip _audioClip;
        public int CurrentSample;
        public bool Mute;
        
        public static int SAMPLE_COUNT = 44100 / 20;
        public static int HISTORY_LENGTH = 20;
        private float[] _samples = new float[SAMPLE_COUNT];
        public List<float> Levels = new List<float>(HISTORY_LENGTH);
        public UnityEvent<float> CurrentMaxLevelChanged = new UnityEvent<float>();

        public void OnEnable()
        {
            Levels.Clear();
            while (Levels.Count < SAMPLE_COUNT)
            {
                Levels.Add(0);
            }
            _audioSource.loop = true;
            _audioClip = Microphone.Start(null, true, 1, 44100);
            _audioSource.clip = _audioClip;
            if (Mute) return;
            while (!(Microphone.GetPosition(null) > 0)) {}
            _audioSource.Play();
        }

        public void Update()
        {
            var microphonePos = Microphone.GetPosition(null);
            var sample = Mathf.FloorToInt(microphonePos / SAMPLE_COUNT);
            if (CurrentSample == sample || sample == 0) return;
            CurrentSample = sample * SAMPLE_COUNT;
            _audioClip.GetData(_samples, CurrentSample - SAMPLE_COUNT);
            var maxLevel = 0f;
            for (var i = 0 ; i < SAMPLE_COUNT; ++i)
            {
                var s = _samples[i];
                var level = s * s;
                if (maxLevel < level)
                {
                    maxLevel = level;
                }
            }

            CurrentMaxLevel = Mathf.Sqrt(maxLevel);
            Levels.Insert(0, CurrentMaxLevel);
            while (Levels.Count > HISTORY_LENGTH)
            {
                Levels.RemoveAt(Levels.Count - 1);
            }
            CurrentMaxLevelChanged.Invoke(maxLevel);
        }

        public float CurrentMaxLevel { get; set; }
    }
}
