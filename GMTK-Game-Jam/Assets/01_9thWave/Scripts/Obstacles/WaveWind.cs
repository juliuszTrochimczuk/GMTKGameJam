using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Obstacles
{
    public class WaveWind : MonoBehaviour
    {
        [SerializeField] private float _windDelay;
        [SerializeField] private float _windStrength;
        [SerializeField] private float _windDuration;

        private AreaEffector2D _areaEffector;

        [SerializeField] private UnityEvent _onWindStart;
        [SerializeField] private UnityEvent _onWindEnd;

        private void Awake()
        {
            _areaEffector = GetComponent<AreaEffector2D>();
            _areaEffector.enabled = false;
            _areaEffector.forceMagnitude = _windStrength;
        }

        public void StartWind() => StartCoroutine(WindBlow());

        private IEnumerator WindBlow()
        {
            yield return new WaitForSeconds(_windDelay);

            _onWindStart.Invoke();
            _areaEffector.enabled = true;

            yield return new WaitForSeconds(_windDuration);

            _areaEffector.enabled = false;
            _onWindEnd.Invoke();
        }
    }
}