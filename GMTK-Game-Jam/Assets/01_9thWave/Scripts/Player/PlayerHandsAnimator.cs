using System;
using UnityEngine;
using UnityEngine.Android;

namespace _01_9thWave.Scripts.Player
{
    public class PlayerHandsAnimator : MonoBehaviour
    {
        [SerializeField] private GameObject _handR;
        [SerializeField] private GameObject _handL;
        [SerializeField] private GameObject _mousePoint;
        [SerializeField] float _clawRotationSpeed = 5f;

        private int rotationOffset;
        private void FixedUpdate()
        {
            Vector3 dirR = _handR.transform.position - _mousePoint.transform.position;
            float angleR = Mathf.Atan2(dirR.y, dirR.x) * Mathf.Rad2Deg;
            Quaternion targetRotationR = Quaternion.Euler(0, 0, angleR-90);
            _handR.transform.rotation = Quaternion.Lerp(_handR.transform.rotation, targetRotationR, Time.deltaTime * _clawRotationSpeed);


            if (transform.localScale.x < 0)
            {
                rotationOffset = -30;
            }
            else
            {
                rotationOffset = 30;
            }
            Vector3 dirL = _handL.transform.position - _mousePoint.transform.position;
            float angleL = Mathf.Atan2(dirL.y, dirL.x) * Mathf.Rad2Deg;
            Quaternion targetRotationL = Quaternion.Euler(0, 0, angleL-90+rotationOffset);
            _handL.transform.rotation = Quaternion.Lerp(_handL.transform.rotation , targetRotationL, Time.deltaTime * _clawRotationSpeed);
            
        }
        
    }
}