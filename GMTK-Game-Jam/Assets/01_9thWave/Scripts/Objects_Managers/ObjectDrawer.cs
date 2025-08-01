using EventsManagers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WavesManagers;

namespace ObjectsManagers
{
    public class ObjectDrawer : MonoBehaviour
    {
        [System.Serializable]
        private class WaveObjectsPool
        {
            public int minSpawnObjectsNumber;
            public int maxSpawnObjectsNumber;

            public List<GameObject> objects;
        }

        [SerializeField] private SpaceArea spawningArea;

        [SerializeField] private List<WaveObjectsPool> objectsPool;

        private int CurrentWave => (WaveCounter.Instance.WaveNumber - 1);

        private void Start() =>
            EventsCaller.Instance.GetEvent(EventsManagers.EventType.Object_Spawn)
            .AddListenerToGameEvent(CreateObjectsFromPoolBasedOnWave);

        public void CreateObjectsFromPoolBasedOnWave()
        {
            int objectsCount = Random.Range(objectsPool[CurrentWave].minSpawnObjectsNumber, objectsPool[CurrentWave].maxSpawnObjectsNumber);
            for (int i = 0; i < objectsCount; i++)
            {
                int objectIndex = Random.Range(0, objectsPool[CurrentWave].objects.Count);
                Instantiate(objectsPool[CurrentWave].objects[objectIndex], spawningArea.GetRandomPosition(), Quaternion.identity);
            }
        }

        public void StopSpawnObjectsAfterMaxWave() => 
            EventsCaller.Instance.GetEvent(EventsManagers.EventType.Object_Spawn)
            .RemoveListenerFromGameEvent(CreateObjectsFromPoolBasedOnWave);
    }
}