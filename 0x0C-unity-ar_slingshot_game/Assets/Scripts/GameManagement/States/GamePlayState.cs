using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.AI;
public class GamePlayState : State
{
    private TargetManager targetManager;
    private GameObject stateUI;
    private GameObject ammoLauncher;
    private Button restartButton;
    private Button quitButton;

    public GamePlayState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void Tick()
    {
    }

    public override void OnStateEnter()
    {
        ammoLauncher = gameManager.ammoLauncher;

        foreach (var UIElement in gameManager.UIList)
            if (UIElement.name == "GamePlayUI")
                stateUI = UIElement;
        if (stateUI)
            stateUI.SetActive(true);
        ammoLauncher.SetActive(true);
    }

    public override void OnStateExit()
    {
        ammoLauncher.SetActive(false);
        stateUI.SetActive(false);
    }
}
