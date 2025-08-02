using System;
using System.Collections;
using System.Collections.Generic;
using _01_9thWave.Scripts.Events_Managers;
using UnityEngine;
using UnityEngine.Events;

namespace EventsManagers
{
    [Serializable]
    public class InGameEvent
    {
        [SerializeField] private float eventCooldown;
        [SerializeField] private float eventDelay;
        [SerializeField] private bool timerRunOnGameStart;

        [SerializeField] private UnityEvent gameEvent;
        [SerializeField] private UIEvent uiEvent;
        public void AddListenerToGameEvent(UnityAction action) => gameEvent.AddListener(action);
        public void RemoveListenerFromGameEvent(UnityAction action) => gameEvent.RemoveListener(action);

        public MonoBehaviour Owner { get; set; }
        public bool TimerRunOnGameStart => timerRunOnGameStart;

        private Coroutine timer;

        public void SetEventCooldown(float newEventCooldown) => eventCooldown = newEventCooldown;

        public void StartTimer()
        {
            if (timer != null)
            {
                Debug.LogError("You try to start timer that is already works");
                return;
            }
            timer = Owner.StartCoroutine(TimerHandler());
        }

        public void StopTimer()
        {
            if (timer == null)
            {
                Debug.LogError("You try to stop timer that is not existing");
                return;
            }
            Owner.StopCoroutine(timer);
            timer = null;
        }

        private IEnumerator TimerHandler()
        {
            yield return new WaitForSeconds(eventDelay);
            while (true)
            {
                gameEvent.Invoke();
                uiEvent.Invoke(eventCooldown);
                yield return new WaitForSeconds(eventCooldown);
            }
        }
    }
}