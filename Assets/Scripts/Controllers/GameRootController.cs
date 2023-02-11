using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameRootController : RootController<GameControllerType>
{
    [Header("Controllers")]
    [SerializeField]
    protected GamePlayController gamePlayController;
    public UnityEvent<GameControllerType> onChangeController;

    private void Start()
    {
        gamePlayController.root = this;
        ChangeController(GameControllerType.GamePlay);
    }

    public override void ChangeController(GameControllerType controller)
    {
        DisengageControllers();
        switch (controller)
        {
            case GameControllerType.GamePlay:
                gamePlayController.EngageController();
                break;
            default: break;
        }
        onChangeController.Invoke(controller);
    }

    public void MainChangeController(GameControllerType controller)
    {
        ChangeController(controller);
    }

    public override void DisengageControllers()
    {

    }

}