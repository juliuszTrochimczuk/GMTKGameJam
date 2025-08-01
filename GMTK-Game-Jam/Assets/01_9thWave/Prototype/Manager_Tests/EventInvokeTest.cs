using EventsManagers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.ManagerTest
{
    public class EventInvokeTest : MonoBehaviour
    {
        private void Start()
        {
            EventsCaller.Instance.GetEvent(EventsManagers.EventType.Object_Spawn).AddListenerToGameEvent(() => PrintToConsole("XDD"));
        }

        public void PrintToConsole(string message)
        {
            Debug.Log(message);
        }
    }
}