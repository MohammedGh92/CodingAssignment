using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIGamePlayRoot : UIRoot
{
    // Reference to menu view class.
    [SerializeField]
    private UIGamePlayView gamePlayView;
    public UIGamePlayView GamePlayView => gamePlayView;

    public override void ShowRoot()
    {
        base.ShowRoot();
        gamePlayView.ShowView();
    }

    public override void HideRoot()
    {
        gamePlayView.HideView();
        base.HideRoot();
    }
}
