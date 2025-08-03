using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TutorialTextsController : MonoBehaviour
    {
        [Serializable]
        private class  TutorialText
        {
            [TextArea][SerializeField] public string text;
            [SerializeField] public float duration;
        }

        [SerializeField] private TutorialText[] tutorial;
        [SerializeField] private float alphaTransitionTime;

        private TextMeshProUGUI uiText;


        private void Awake()
        {
            uiText = GetComponent<TextMeshProUGUI>();
            uiText.color = new(uiText.color.r, uiText.color.g, uiText.color.b, 0);
        }

        private void Start()
        {
            StartCoroutine(HandleTexts());
        }

        private IEnumerator HandleTexts()
        {
            for (int i = 0; i < tutorial.Length; i++)
            {
                uiText.text = tutorial[i].text;
                yield return StartCoroutine(AlphaTransition(1));
                yield return new WaitForSeconds(tutorial[i].duration);
                yield return StartCoroutine(AlphaTransition(0));
            }
            Destroy(gameObject.transform.parent.gameObject);
        }

        private IEnumerator AlphaTransition(float targetAlpha)
        {
            Color oldColor = uiText.color;
            Color newColor = new(uiText.color.r, uiText.color.g, uiText.color.b, targetAlpha);
            for (float time = 0.0f; time <= alphaTransitionTime; time += Time.deltaTime)
            {
                uiText.color = Color.Lerp(oldColor, newColor, time / alphaTransitionTime);
                yield return new WaitForEndOfFrame();
            }
            uiText.color = newColor;
        }
    }
}