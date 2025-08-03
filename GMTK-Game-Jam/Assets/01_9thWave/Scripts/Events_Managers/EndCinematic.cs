namespace _01_9thWave.Scripts.Events_Managers
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.Video;

    [RequireComponent(typeof(VideoPlayer))]
    public class VideoEndOrSkip : MonoBehaviour
    {
        private VideoPlayer videoPlayer;

        void Start()
        {
            videoPlayer = GetComponent<VideoPlayer>();

            if (videoPlayer != null)
            {
                videoPlayer.loopPointReached += OnVideoFinished;
            }
            else
            {
                Debug.LogError("VideoPlayer component not found on the object.");
            }
        }

        void Update()
        {
#if UNITY_WEBGL
            LoadNextScene();
#endif
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                LoadNextScene();
            }
        }

        void OnVideoFinished(VideoPlayer vp)
        {
            LoadNextScene();
        }

        void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int totalScenes = SceneManager.sceneCountInBuildSettings;

            if (currentSceneIndex + 1 < totalScenes)
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(0); // Loop back to the first scene
            }
        }
    }
}