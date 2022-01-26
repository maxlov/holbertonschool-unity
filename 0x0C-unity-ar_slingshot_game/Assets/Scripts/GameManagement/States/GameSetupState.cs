using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.AI;
public class GameSetupState : State
{
    private ARPlane gamePlane;
    private NavMeshSurface gameNavSurface;
    private TargetManager targetManager;
    private GameObject stateUI;
    private Button startButton;

    public GameSetupState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void Tick()
    {
    }


    public override void OnStateEnter()
    {
        gamePlane = gameManager.gamePlane;
        targetManager = gameManager.targetManager;
        gameNavSurface = gamePlane.GetComponent<NavMeshSurface>();

        gameNavSurface.BuildNavMesh();
        targetManager.SpawnTargets(gamePlane.gameObject);

        foreach (var UIElement in gameManager.UIList)
            if (UIElement.name == "GameSetupUI")
                stateUI = UIElement;
        if (stateUI)
            stateUI.SetActive(true);

        startButton = stateUI.GetComponentInChildren<Button>();
        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        gameManager.SetState(new GamePlayState(gameManager));
    }

    public override void OnStateExit()
    {
        startButton.onClick.RemoveAllListeners();
        stateUI.SetActive(false);
    }
}
