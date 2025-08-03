using System.Collections.Generic;
using _01_9thWave.Scripts.Singleton;
using UnityEngine;

namespace _01_9thWave.Scripts.UI
{
    public class UIManager : Singleton<UIManager>
    {
        private readonly List<GameObject> _uiElements = new();
    
        private readonly List<GameObject> _lockedUiElements = new();
    
        public void AddToUIManagerList(GameObject uiElement) 
        {
            if (_uiElements.Contains(uiElement)) 
            {
                return;
            }

            _uiElements.Add(uiElement);
        }
    
        public void AddToUIManagerLockedList(GameObject uiElement) 
        {
            if (_uiElements.Contains(uiElement)) 
            {
                return;
            }

            _lockedUiElements.Add(uiElement);
        }

        public void RemoveFromUIManagerList(GameObject uiElement)
        {
            _uiElements.Remove(uiElement);
        }

        public GameObject GetUIElementWithComponent<T>() 
        {
            return _uiElements.Find(uiElement => uiElement.GetComponent<T>() is not null);
        }

        public T GetUIComponent<T>()
        {
            return _uiElements.Find(uiElement => uiElement.GetComponent<T>() is not null).GetComponent<T>();
        }
    
        public T GetLockedUIComponent<T>()
        {
            return _lockedUiElements.Find(uiElement => uiElement.GetComponent<T>() is not null).GetComponent<T>();
        }

        public void ShowSingleUIElement(GameObject gameObject) 
        {
            foreach (GameObject uiElement in _uiElements)
            {
                if (uiElement != gameObject)
                {
                    uiElement.SetActive(false);
                }
            }
        }

        public void ShowAllUIElements()
        {
            foreach (GameObject uiElement in _uiElements)
            {
                uiElement.SetActive(true);
            }
        }
    }
}