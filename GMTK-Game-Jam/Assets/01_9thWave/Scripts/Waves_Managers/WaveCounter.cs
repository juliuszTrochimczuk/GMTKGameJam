using EventsManagers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WavesManagers
{
    public class WaveCounter : MonoBehaviour
    {
        [SerializeField] private UnityEvent WhenMaxWaveAchived;

        [SerializeField] private int maxWave;

        private int waveNumber;

        public int WaveNumber 
        {
            get => waveNumber;
            set
            {
                if (value > maxWave)
                    waveNumber = maxWave;
                else if (value < 0)
                    waveNumber = 0;
                else
                    waveNumber = value;
            }
        }

        public static WaveCounter Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance);
            Instance = this;

            EventsCaller.Instance.GetEvent(EventsManagers.EventType.Wave).AddListenerToGameEvent(IncreaseWaveNumber);
        }

        public void IncreaseWaveNumber()
        {
            if (WaveNumber == maxWave)
            {
                WaveNumber = maxWave;
                WhenMaxWaveAchived.Invoke();

                EventsCaller.Instance.GetEvent(EventsManagers.EventType.Wave).RemoveListenerFromGameEvent(IncreaseWaveNumber);
            }
            else
                WaveNumber++;
        }

        public void ResetWaveNumber() => WaveNumber = 0;
    }
}