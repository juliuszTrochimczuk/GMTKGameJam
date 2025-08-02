using System;
using TMPro;
using UnityEngine;

namespace _01_9thWave.Scripts.UI
{
    public class WaveCountUI : MonoBehaviour
    {
        [SerializeField] private GameObject currentWaveCountObject;
        [SerializeField] private GameObject maxWaveCountObject;
        
        private int _currentWaveCount = 0;
        private int _maxWaveCount = 0;
        
        public void UpdateWaveCounts(int currentWaveNumber, int maxWaveNumber)
        {
            _currentWaveCount = currentWaveNumber;
            _maxWaveCount = maxWaveNumber;
            UpdateText();
        }

        public void UpdateCurrentWaveCount(int waveNumber)
        {
            _currentWaveCount = waveNumber;
            UpdateText();
        }
        
        public void UpdateMaxWaveCount(int waveNumber)
        {
            _maxWaveCount = waveNumber;
            UpdateText();
        }

        public void ResetWaveCount()
        {
            _currentWaveCount = 1;
            UpdateText();
        }

        private void UpdateText()
        {
            currentWaveCountObject.GetComponent<TMP_Text>().text = _currentWaveCount.ToString();
            maxWaveCountObject.GetComponent<TMP_Text>().text = _maxWaveCount.ToString();
        }
    }
}
