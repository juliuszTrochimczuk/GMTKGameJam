using System.Collections;
using System.Collections.Generic;
using _01_9thWave.Scripts.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
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
    }
}