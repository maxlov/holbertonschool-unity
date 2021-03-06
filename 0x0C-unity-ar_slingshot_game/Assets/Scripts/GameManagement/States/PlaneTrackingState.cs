using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneTrackingState : State
{
    private ARPlaneManager planeManager;
    private GameObject stateUI;

    public PlaneTrackingState(GameManager gameManager) : base(gameManager)
    {
        planeManager = gameManager.planeManager;
        if (planeManager.enabled == false)
            planeManager.enabled = true;
    }

    public override void Tick()
    {
        if (planeManager.trackables.count > 0)
            gameManager.SetState(new PlaneSetupState(gameManager));
    }

    public override void OnStateEnter()
    {
        foreach (var UIElement in gameManager.UIList)
            if (UIElement.name == "PlaneTrackingUI")
                stateUI = UIElement;
        stateUI.SetActive(true);
    }

    public override void OnStateExit()
    {
        stateUI.SetActive(false);
    }
}
