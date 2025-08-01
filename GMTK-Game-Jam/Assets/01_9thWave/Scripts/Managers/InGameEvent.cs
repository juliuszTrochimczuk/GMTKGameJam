using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    [Serializable]
    public class InGameEvent
    {
        [SerializeField] private float eventCooldown;
        [SerializeField] private bool timerRunOnGameStart;

        [SerializeField] private UnityEvent gameEvent;
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
            while (true)
            {
                gameEvent.Invoke();
                yield return new WaitForSeconds(eventCooldown);
            }
        }
    }
}