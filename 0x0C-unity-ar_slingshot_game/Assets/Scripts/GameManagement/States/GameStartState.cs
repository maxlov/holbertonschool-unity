using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameStartState : State
{
    private ARPlaneManager planeManager;
    private GameObject stateUI;

    public GameStartState(GameManager gameManager) : base(gameManager)
    {
        planeManager = gameManager.planeManager;
    }

    public override void Tick()
    {
        if (planeManager.trackables.count > 0)
            gameManager.SetState(new GameSetupState(gameManager));
    }

    public override void OnStateEnter()
    {
        foreach (var UIElement in gameManager.UIList)
            if (UIElement.name == "StartUI")
                stateUI = UIElement;
        stateUI.SetActive(true);
    }

    public override void OnStateExit()
    {
        stateUI.SetActive(false);
    }
}
