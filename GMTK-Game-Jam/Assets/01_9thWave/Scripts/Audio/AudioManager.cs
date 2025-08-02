using UnityEngine;
using UnityEngine.Audio;

namespace _01_9thWave.Scripts.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        
        [Header("Background Music")]
        [SerializeField] private AudioSource _backgroundMusicSource;
        
        [Header("Sound Effects")]
        [SerializeField] private AudioSource _footstepSource;
        
        public void SetMasterVolume(float volume) { _audioMixer.SetFloat("masterVolume", volume); }
    
        public void SetSoundEffectsVolume(float volume) { _audioMixer.SetFloat("soundEffectsVolume", volume); }
    
        public void SetMusicVolume(float volume) { _audioMixer.SetFloat("musicVolume", volume); }

        public void PlayBackgroundMusic() { _backgroundMusicSource.Play(); }

        public void StopBackgroundMusic() { _backgroundMusicSource.Stop(); }
        
        public void SetBackgroundMusicSource(AudioSource musicSource) { _backgroundMusicSource = musicSource; }
    }
}
