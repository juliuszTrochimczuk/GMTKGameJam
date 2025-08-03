using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cutscenes
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
    }
}