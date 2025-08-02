using UnityEngine;

namespace _01_9thWave.Scripts.Game_Managers
{
    public class GameStateManager : MonoBehaviour
    {
        private bool _gameIsPaused = false;
        
        public void PauseGame()
        {
            if (_gameIsPaused)
            {
                _gameIsPaused = !_gameIsPaused;
                Time.timeScale = 1f;
            }
            else
            {
                _gameIsPaused = !_gameIsPaused;
                Time.timeScale = 0f;
            }
        }
    }
}
