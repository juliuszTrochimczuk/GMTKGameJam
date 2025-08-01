using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Managers
{
    public class EventsCaller : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<EventType, InGameEvent> gameEvents;

        public static EventsCaller Instance { get; private set; }

        private void Awake()
        {
            foreach (InGameEvent @event in gameEvents.Values)
            {
                @event.Owner = this;
                if (@event.TimerRunOnGameStart)
                    @event.StartTimer();
            }

            if (Instance != null)
            {
                foreach (InGameEvent @event in gameEvents.Values)
                    @event.StopTimer();
                Destroy(Instance);
            }

            Instance = this;
        }

        public InGameEvent GetEvent(EventType key) => gameEvents[key]; 
    }
}