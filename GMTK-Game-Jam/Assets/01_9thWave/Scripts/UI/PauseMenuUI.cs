using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace _01_9thWave.Scripts.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        [SerializeField] private UnityEvent pauseEvent;
        [SerializeField] private UnityEvent unpauseEvent;

        public void FlipMenu(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                if (gameObject.activeSelf)
                {
                    unpauseEvent.Invoke();
                    gameObject.SetActive(false);
                }
                else
                {
                    pauseEvent.Invoke();
                    gameObject.SetActive(this);
                }
            }
        }
    }
}
