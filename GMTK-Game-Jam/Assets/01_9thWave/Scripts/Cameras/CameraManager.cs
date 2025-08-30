using Cinemachine;
using UnityEngine;

namespace _01_9thWave.Scripts.Cameras
{
    public class CameraManager : MonoBehaviour
    {
        static CinemachineVirtualCamera _cachedVirtualCamera;
        private static CinemachineVirtualCamera _activeCamera;

        public static void SwitchCamera(CinemachineVirtualCamera newCamera)
        {
            newCamera.Priority = 10;
            _cachedVirtualCamera = _activeCamera;
            _activeCamera = newCamera;
        
            if(_cachedVirtualCamera)
                _cachedVirtualCamera.Priority = 0;
        }
    
        public static void SwitchCameraBack()
        {
            SwitchCamera(_cachedVirtualCamera);
        }
    }
}
