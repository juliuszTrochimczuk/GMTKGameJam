using _01_9thWave.Scripts.Singleton;
using UnityEngine;
using UnityEngine.Audio;

namespace _01_9thWave.Scripts.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
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
        
        public void SetMasterVolume(float volume) { _audioMixer.SetFloat("masterVolume", volume); }
    
        public void SetSoundEffectsVolume(float volume) { _audioMixer.SetFloat("effectsVolume", volume); }
    
        public void SetMusicVolume(float volume) { _audioMixer.SetFloat("musicVolume", volume); }

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

        public void PlayClickSound() { _clickSource.Play(); }
        public void StopBackgroundMusic() { _backgroundMusicSource.Stop(); }
        
        public void SetBackgroundMusicSource(AudioSource musicSource) { _backgroundMusicSource = musicSource; }
    }
}
