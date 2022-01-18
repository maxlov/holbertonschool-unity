using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameStartState : State
{
    private ARPlaneManager planeManager;

    public GameStartState(GameManager gameManager) : base(gameManager)
    {
        planeManager = gameManager.planeManager;
    }

    public override void Tick()
    {
        if (planeManager.trackables.count > 0)
            gameManager.SetState(new GameSetupState(gameManager));
    }

    public override void OnStateExit()
    {
        gameManager.UIAssets["SelectUI"].SetActive(false);
    }
}
