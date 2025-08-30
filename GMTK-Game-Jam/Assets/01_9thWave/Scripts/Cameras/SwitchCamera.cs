using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace _01_9thWave.Scripts.Cameras
{
    public class SwitchCamera : MonoBehaviour
    {
        public CinemachineVirtualCamera vcam1;
        public CinemachineVirtualCamera vcam2;

        private void Start()
        {
            StartCoroutine(StartCameraCoroutine());
        }

        IEnumerator StartCameraCoroutine()
        {
            yield return new WaitForSeconds(.5f);
            CameraManager.SwitchCamera(vcam2);
            yield return new WaitForSeconds(5.0f);
            CameraManager.SwitchCamera(vcam1);
        }
    }
}
