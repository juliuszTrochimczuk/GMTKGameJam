using _01_9thWave.Scripts.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WavesManagers;

namespace Objects
{
    public class WaveAnimController : MonoBehaviour
    {
        [SerializeField] private float[] _waveHitPitches;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            AudioManager.Instance.PlayBackgroundWaveCalm();
        }

        public void PlayWaveAnimation()
        {
            if (WaveCounter.Instance.IsLastWave)
                _animator.SetTrigger("GAGAWave");
            else
                _animator.SetTrigger("StartWave");
        }

        public void PlayWaveHitSounds()
        {
            AudioManager.Instance.SetWaveHitPitch(_waveHitPitches[WaveCounter.Instance.WaveNumber - 1]);
            AudioManager.Instance.PlayWaveHit();
        }
    }
}