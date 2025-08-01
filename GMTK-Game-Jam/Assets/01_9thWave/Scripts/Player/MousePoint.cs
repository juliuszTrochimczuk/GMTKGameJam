using UnityEngine;

namespace _01_9thWave.Scripts.Player
{
    public class MousePoint : MonoBehaviour
    {
        void Update()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            transform.position = mousePos;
        }
    }
}
