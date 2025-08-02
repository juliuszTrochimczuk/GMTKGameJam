using _01_9thWave.Scripts.Audio;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

        public void LoadScene(string sceneName)
        {
            AudioManager.Instance.PlayClickSound();
            SceneManager.LoadScene(sceneName);
            unpauseEvent.Invoke();
        }
    }
}
