using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameSetupState : State
{
    private ARPlaneManager planeManager;
    private GameObject stateUI;

    public GameSetupState(GameManager gameManager) : base(gameManager)
    {
        planeManager = gameManager.planeManager;
    }

    public override void Tick()
    {

    }

    public override void OnStateEnter()
    {
        foreach (var UIElement in gameManager.UIList)
            if (UIElement.name == "SetupUI")
                stateUI = UIElement;
        if (stateUI)
            stateUI.SetActive(true);
    }

    public override void OnStateExit()
    {
        stateUI.SetActive(false);
    }
}
