using System;
using System.Collections;
using System.Collections.Generic;
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

        [SerializeField] private CutsceneShot[] shots;
        [SerializeField] private float _shotsTransitionTime;

        private Image _image;

        [SerializeField] private UnityEvent onCutsceneEnd;

        private void Awake()
        {
            _image = GetComponentInChildren<Image>();
            _image.sprite = shots[0].shot;
        }
        
        private void Start() => StartCoroutine(Cutscene());

        private IEnumerator Cutscene()
        {
            yield return new WaitForSeconds(shots[0].duration);
            for (int i = 1; i < shots.Length; i++)
            {
                yield return StartCoroutine(Transition(0));
                _image.sprite = shots[i].shot;
                yield return StartCoroutine(Transition(1));
                yield return new WaitForSeconds(shots[i].duration);
            }
            yield return StartCoroutine(Transition(0));
            onCutsceneEnd.Invoke();
        }

        private IEnumerator Transition(float targetAlpha)
        {
            Color oldColor = _image.color;
            Color newColor = new(_image.color.r, _image.color.g, _image.color.b, targetAlpha);
            for (float time = 0.0f; time <= _shotsTransitionTime; time += Time.deltaTime)
            {
                _image.color = Color.Lerp(oldColor, newColor, time / _shotsTransitionTime);
                yield return new WaitForEndOfFrame();
            }
            _image.color = newColor;
        }
    }
}