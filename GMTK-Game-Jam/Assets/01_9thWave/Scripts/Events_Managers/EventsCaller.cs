using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace EventsManagers
{
    public class EventsCaller : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<EventType, InGameEvent> gameEvents;

        public static EventsCaller Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                foreach (InGameEvent @event in Instance.gameEvents.Values)
                    @event.StopTimer();
                Destroy(Instance);
            }

            Instance = this;
        }

        private void Start()
        {
            foreach (InGameEvent @event in gameEvents.Values)
            {
                @event.Owner = this;
                if (@event.TimerRunOnGameStart)
                    @event.StartTimer();
            }
        }

        public InGameEvent GetEvent(EventType key) => gameEvents[key]; 
    }
}