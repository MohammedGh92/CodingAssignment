using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManagerScr : Singleton<InputManagerScr>
{

    private bool callOnce;
    [SerializeField]
    private UnityEvent escClicked;

    private void Update()
    {
        if (!callOnce && Input.GetKeyDown(KeyCode.Escape))
        {
            escClicked.Invoke();
            callOnce = true;
        }
    }

}