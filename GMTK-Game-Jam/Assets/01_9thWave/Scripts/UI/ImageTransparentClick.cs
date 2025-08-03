using UnityEngine;
using UnityEngine.UI;

namespace _01_9thWave.Scripts.UI
{
    public class ImageTransparentClick : MonoBehaviour
    {
        private void OnEnable()
        {
            if (!gameObject.GetComponent<Image>())
            {
                Debug.Log("Not image");
            }
            else
            {
                gameObject.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.001f;
            }
        }
    }
}
