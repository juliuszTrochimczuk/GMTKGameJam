using System.Collections;
using System.Collections.Generic;
using _01_9thWave.Scripts.Audio;
using _01_9thWave.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject menu;
        private void Start()
        {
            UIManager.Instance.AddToUIManagerList(gameObject);
        }
        public void LoadScene(string sceneName)
        {
            AudioManager.Instance.PlayClickSound();
            SceneManager.LoadScene(sceneName);
        }

        public void ExitApp()
        {
            AudioManager.Instance.PlayClickSound();
            Application.Quit();
        }
        
        public void ShowMainMenu()
        {
            menu.SetActive(true);
            UIManager.Instance.ShowSingleUIElement(gameObject);
        }
        
        public void HideMainMenu()
        {
            menu.SetActive(false);
            UIManager.Instance.ShowAllUIElements();
        }
    }
}