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
        [SerializeField] private GameObject masterVolumeObject;
        
        [SerializeField] private UnityEvent<float> sliderEvent;
        
        public void OnPointerUp(PointerEventData eventData)
        {
            sliderEvent.Invoke(Mathf.Log10(masterVolumeObject.GetComponent<Slider>().value) * 20);
        }
    }
}
