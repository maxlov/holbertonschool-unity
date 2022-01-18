using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameSetupState : State
{
    private ARPlaneManager planeManager;

    public GameSetupState(GameManager gameManager) : base(gameManager)
    {
        planeManager = gameManager.planeManager;
    }

    public override void Tick()
    {
        
    }
}
