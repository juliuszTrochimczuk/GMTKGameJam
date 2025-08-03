using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _01_9thWave.Scripts.UI
{
    public class WaveCooldownBar : MonoBehaviour
    {
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }
        
        private void Start()
        {
            UIManager.Instance.AddToUIManagerList(gameObject);
        }
        
        public void StartCooldownBar(float fillTime) => StartCoroutine(FillCooldownBar(fillTime));
        
        IEnumerator FillCooldownBar(float fillTime)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fillTime)
            {                
                _slider.value = Mathf.Lerp(0f, 1f, elapsedTime / fillTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            _slider.value = 1f;
        }
        
        public void ResetCooldownBar()
        {
            _slider.value = _slider.minValue;
        }
    }
}
