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
        [SerializeField] private GameObject settingsMenu;
        [SerializeField] private UnityEvent pauseEvent;
        [SerializeField] private UnityEvent unpauseEvent;
        
        private void Start()
        {
            UIManager.Instance.AddToUIManagerList(gameObject);
        }

        public void FlipMenu(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
                FlipMenu();
        }
        
        public void FlipMenu()
        {
            if (pauseMenu.activeSelf || settingsMenu.activeSelf)
            {
                unpauseEvent.Invoke();
                HidePauseMenu();
            }
            else
            {
                pauseEvent.Invoke();
                ShowPauseMenu();
            }
        }
        
        public void ShowPauseMenu()
        {
            pauseMenu.SetActive(true);
            UIManager.Instance.ShowSingleUIElement(gameObject);
        }
        
        public void HidePauseMenu()
        {
            pauseMenu.SetActive(false);
            UIManager.Instance.ShowAllUIElements();
        }

        public void LoadScene(string sceneName)
        {
            AudioManager.Instance.PlayClickSound();
            SceneManager.LoadScene(sceneName);
            unpauseEvent.Invoke();
        }
    }
}
