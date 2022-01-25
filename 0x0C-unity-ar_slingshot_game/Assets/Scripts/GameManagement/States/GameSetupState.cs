using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.AI;
public class GameSetupState : State
{
    private ARPlane gamePlane;
    private NavMeshSurface gameNavSurface;
    private GameObject stateUI;

    public GameSetupState(GameManager gameManager) : base(gameManager)
    {
        gamePlane = gameManager.gamePlane;
        gameNavSurface = gamePlane.GetComponent<NavMeshSurface>();
    }

    public override void Tick()
    {
    }


    public override void OnStateEnter()
    {
        foreach (var UIElement in gameManager.UIList)
            if (UIElement.name == "GameSetupUI")
                stateUI = UIElement;
        if (stateUI)
            stateUI.SetActive(true);
    }

    public override void OnStateExit()
    {
        stateUI.SetActive(false);
    }
}
