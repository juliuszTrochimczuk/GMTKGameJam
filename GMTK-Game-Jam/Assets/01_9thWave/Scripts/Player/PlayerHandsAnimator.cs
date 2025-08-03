using System;
using System.Collections;
using UnityEngine;
namespace _01_9thWave.Scripts.Player
{
    public class PlayerHandsAnimator : MonoBehaviour
    {
        [SerializeField] private GameObject _handR;
        [SerializeField] private GameObject _handL;
        [SerializeField] private GameObject _mousePoint;
        [SerializeField] float _clawRotationSpeed = 5f;
        [SerializeField] private Sprite _clawTextureROpen;
        [SerializeField] private Sprite _clawTextureLOpen;
        [SerializeField] private Sprite _clawTextureRClosed;
        [SerializeField] private Sprite _clawTextureLClosed;
        private int rotationOffset;
        private bool _clawsClosed = true;
        private void FixedUpdate()
        {
            Vector3 dirR = _handR.transform.position - _mousePoint.transform.position;
            float angleR = Mathf.Atan2(dirR.y, dirR.x) * Mathf.Rad2Deg;
            Quaternion targetRotationR = Quaternion.Euler(0, 0, angleR-90);
            _handR.transform.rotation = Quaternion.Lerp(_handR.transform.rotation, targetRotationR, Time.deltaTime * _clawRotationSpeed);


            if (transform.localScale.x < 0)
            {
                rotationOffset = 0;
            }
            else
            {
                rotationOffset = 0;
            }
            Vector3 dirL = _handL.transform.position - _mousePoint.transform.position;
            float angleL = Mathf.Atan2(dirL.y, dirL.x) * Mathf.Rad2Deg;
            Quaternion targetRotationL = Quaternion.Euler(0, 0, angleL-90+rotationOffset);
            _handL.transform.rotation = Quaternion.Lerp(_handL.transform.rotation , targetRotationL, Time.deltaTime * _clawRotationSpeed);
            
        }

        public void SwichClawState()
        {
            if (_clawsClosed)
            {
                _clawsClosed = false;
                OpenClaws();
            }
            else if (!_clawsClosed)
            {
                _clawsClosed = true;
                CloseClaws();
            }
        }

        private void OpenClaws()
        {
            _handR.GetComponentInChildren<SpriteRenderer>().sprite = _clawTextureROpen;
            _handL.GetComponentInChildren<SpriteRenderer>().sprite = _clawTextureLOpen;
        }

        private void CloseClaws()
        {
            _handR.GetComponentInChildren<SpriteRenderer>().sprite = _clawTextureRClosed;
            _handL.GetComponentInChildren<SpriteRenderer>().sprite = _clawTextureLClosed;
        }
        
        public void DisplayClaws()
        {
            StartCoroutine(DisplayClawsOperation());
        }
        
        public void HideClaws()
        {
            _handR.SetActive(false);
            _handL.SetActive(false);
        }

        private IEnumerator DisplayClawsOperation()
        {
            yield return new WaitForSeconds(0.8f);
            _handR.SetActive(true);
            _handL.SetActive(true);
        }
    }
}