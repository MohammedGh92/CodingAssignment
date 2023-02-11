using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private const float quitDelayTime = 0.2f;

    public void QuitGame()
    {
        StartCoroutine(QuitGameCor());
    }

    private IEnumerator QuitGameCor()
    {
        yield return new WaitForSeconds(quitDelayTime);
        Application.Quit();
    }

}
