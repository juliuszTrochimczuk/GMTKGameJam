using _01_9thWave.Scripts.Audio;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _01_9thWave.Scripts.UI
{
    public class VolumeSliderUI : MonoBehaviour, IPointerUpHandler
    {
        [SerializeField] private AudioManager.VolumeType volumeType;

        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.value = PlayerPrefs.GetFloat(volumeType.ToString(), 1.0f);
        }

        public void ChangeVolume(float value)
        {
            PlayerPrefs.SetFloat(volumeType.ToString(), value);
            value = Mathf.Log10(value) * 20;
            AudioManager.Instance.SetMixerVolume(volumeType, value);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            AudioManager.Instance.PlayClickSound();
        }
    }
}
