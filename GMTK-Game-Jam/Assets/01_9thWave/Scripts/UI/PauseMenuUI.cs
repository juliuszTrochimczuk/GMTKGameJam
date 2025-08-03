using _01_9thWave.Scripts.Audio;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace _01_9thWave.Scripts.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private UnityEvent pauseEvent;
        [SerializeField] private UnityEvent unpauseEvent;
        
        private void Start()
        {
            UIManager.Instance.AddToUIManagerList(gameObject);
        }

        public void FlipMenu(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                if (pauseMenu.activeSelf)
                {
                    unpauseEvent.Invoke();
                    pauseMenu.SetActive(false);
                    UIManager.Instance.ShowAllUIElements();
                }
                else
                {
                    pauseEvent.Invoke();
                    pauseMenu.SetActive(true);
                    UIManager.Instance.ShowSingleUIElement(gameObject);
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
