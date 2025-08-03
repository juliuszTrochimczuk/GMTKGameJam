using UnityEngine;

namespace _01_9thWave.Scripts.UI
{
    public class SettingsMenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject settingsMenu;
        private void Start()
        {
            UIManager.Instance.AddToUIManagerList(gameObject);
        }

        public void ShowSettingsMenu()
        {
            settingsMenu.SetActive(true);
            UIManager.Instance.ShowSingleUIElement(gameObject);
        }
        
        public void HideSettingsMenu()
        {
            settingsMenu.SetActive(false);
            UIManager.Instance.ShowAllUIElements();
        }
    }
}
