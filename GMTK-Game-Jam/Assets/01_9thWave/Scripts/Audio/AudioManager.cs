using _01_9thWave.Scripts.Singleton;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;

namespace _01_9thWave.Scripts.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        public enum VolumeType { Master, SFX, Music }

        [SerializeField] private AudioMixer _audioMixer;
        
        [Header("Background Music")]
        [SerializeField] private AudioSource _backgroundMusicSource;
        
        [Header("Game Sound Effects")]
        [SerializeField] private AudioSource _footstepSource;
        [SerializeField] private AudioSource _jellyfishJumpSource;
        [SerializeField] private AudioSource _bombExplosionSource;
        [SerializeField] private AudioSource _backgroundWaveCalm;
        [SerializeField] private AudioSource _waveHits;
        [SerializeField] private AudioSource _pufferFishSource;
        
        [Header("Menu Sound Effects")]
        [SerializeField] private AudioSource _clickSource;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(Instance);
        }

        private void Start()
        {
            PlayBackgroundMusic();

            _audioMixer.SetFloat("masterVolume", Mathf.Log10(PlayerPrefs.GetFloat("Master", 1)) * 20);
            _audioMixer.SetFloat("effectsVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFX", 1)) * 20);
            _audioMixer.SetFloat("musicVolume", Mathf.Log10(PlayerPrefs.GetFloat("Music", 1)) * 20);
        }

        public void SetMixerVolume(VolumeType volumeType, float volume)
        {
            if (volumeType == VolumeType.Master)
                _audioMixer.SetFloat("masterVolume", volume);
            else if (volumeType == VolumeType.SFX)
                _audioMixer.SetFloat("effectsVolume", volume);
            else if (volumeType == VolumeType.Music)
                _audioMixer.SetFloat("musicVolume", volume);
        }

        public float GetMixerVolume(VolumeType volumeType)
        {
            float value = 0.0f; 
            if (volumeType == VolumeType.Master)
                _audioMixer.GetFloat("masterVolume", out value);
            else if (volumeType == VolumeType.SFX)
                _audioMixer.GetFloat("effectsVolume", out value);
            else if (volumeType == VolumeType.Music)
                _audioMixer.GetFloat("musicVolume", out value);
            return value;
        }

        public void PlayBackgroundMusic() { _backgroundMusicSource.Play(); }

        public void PlayFootstepEffects()
        {
            _footstepSource.pitch = Random.Range(0.8f, 1.2f);
            _footstepSource.Play();
        }
        public void PlayJellyfishJumpSound() { _jellyfishJumpSource.Play(); }
        public void PlayBombExplosionSound() { _bombExplosionSource.Play(); }
        public void PlayBackgroundWaveCalm() { _backgroundWaveCalm.Play(); }
        public void PlayWaveHit() { _waveHits.Play(); }
        public void PlayPufferFishSound() { _pufferFishSource.Play(); }
        public void SetWaveHitPitch(float newPitch) => _waveHits.pitch = newPitch;
        public void SetWaveHitVolume(float newVolume) => _waveHits.volume = newVolume;

        public void PlayClickSound() { _clickSource.Play(); }
        public void StopBackgroundMusic() { _backgroundMusicSource.Stop(); }
        
        public void SetBackgroundMusicSource(AudioSource musicSource) { _backgroundMusicSource = musicSource; }
    }
}
