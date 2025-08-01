using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _01_9thWave.Scripts.Player
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _deadZone;
        [SerializeField] private float cameraSpeed;
        [SerializeField] private Transform player;

        private void FixedUpdate()
        {
            if (Mathf.Abs(transform.position.y - player.position.y) <= _deadZone)
                return;

            float direction = 0;

            if ((transform.position.y - player.position.y) > 0)
                direction = -1;
            else
                direction = 1;
            Debug.Log(direction);

            transform.Translate(Vector2.up * cameraSpeed * direction * Time.fixedDeltaTime);
        }
    }
}