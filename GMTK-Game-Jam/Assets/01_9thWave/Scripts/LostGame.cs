using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LostGame : MonoBehaviour
{
    [SerializeField] private float restartDelay;

    public void RestartGame() => StartCoroutine(RestartGameAfterDelay());

    private IEnumerator RestartGameAfterDelay()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene("FinalScene");
    }
}
