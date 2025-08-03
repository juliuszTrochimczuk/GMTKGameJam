using UnityEngine;
using UnityEngine.Events;

namespace _01_9thWave.Scripts.Objects
{
    public class VictoryZone : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onVictory;
        
        public void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.CompareTag("Player")){
                Debug.Log("Victory");
                _onVictory.Invoke();
            }
        }
        
    }
}
