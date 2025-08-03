using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Cutscenes 
{
    public class CutsceneManager : MonoBehaviour
    {
        [Serializable]
        private class CutsceneShot
        {
            public float duration;
            public Sprite shot;
        }

        [SerializeField] private CutsceneShot[] _shots;
        [SerializeField] private float _shotsTransitionTime;

        [SerializeField] private Image _template;

        [Space(10)]

        [SerializeField] private UnityEvent onCutsceneEnd;

        private List<Image> _plans = new();

        private void Awake()
        {
            for (int i = _shots.Length - 1; i >= 0; i--)
            {
                Image newShot = Instantiate(_template, transform).GetComponent<Image>();
                newShot.sprite = _shots[i].shot;
                _plans.Add(newShot);
            }

            Destroy(_template.gameObject);
        }
        
        private void Start() => StartCoroutine(Cutscene());

        private IEnumerator Cutscene()
        {
            for (int i = _plans.Count - 1; i >= 0; i--)
            {
                _plans[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(_shots[i].duration);
                yield return StartCoroutine(Transition(0, _plans[i]));
            }
            onCutsceneEnd.Invoke();
        }

        private IEnumerator Transition(float targetAlpha, Image imageToTransition)
        {
            Color oldColor = imageToTransition.color;
            Color newColor = new(imageToTransition.color.r, imageToTransition.color.g, imageToTransition.color.b, targetAlpha);
            for (float time = 0.0f; time <= _shotsTransitionTime; time += Time.deltaTime)
            {
                imageToTransition.color = Color.Lerp(oldColor, newColor, time / _shotsTransitionTime);
                yield return new WaitForEndOfFrame();
            }
            imageToTransition.color = newColor;
        }
    }
}