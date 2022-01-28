using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayState : State
{
    private TargetManager targetManager;
    private ScoreManager scoreManager;
    private AmmoLauncher ammoLauncher;

    private GameObject stateUI;
    private Button restartButton;
    private Button quitButton;
    private Button playAgainButton;

    private bool canPlayAgain = false;

    public GamePlayState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void Tick()
    {
        if (canPlayAgain && (targetManager.target_count <= 0 || ammoLauncher.outOfAmmo))
        {
            playAgainButton.gameObject.SetActive(true);
        }
    }

    public override void OnStateEnter()
    {
        ammoLauncher = gameManager.ammoLauncher;
        scoreManager = gameManager.scoreManager;
        targetManager = gameManager.targetManager;

        ammoLauncher.gameObject.SetActive(true);

        foreach (var UIElement in gameManager.UIList)
            if (UIElement.name == "GamePlayUI")
                stateUI = UIElement;
        if (stateUI)
            stateUI.SetActive(true);

        Transform buttonBG = stateUI.transform.Find("GamePlayButtonBG");
        restartButton = buttonBG.Find("GameRestartButton").gameObject.GetComponent<Button>();
        quitButton = buttonBG.Find("GameExitButton").gameObject.GetComponent<Button>();
        playAgainButton = buttonBG.Find("GameReplayButton").gameObject.GetComponent<Button>();

        Debug.Log($"{restartButton}, {quitButton}, {playAgainButton}");

        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(ExitGame);
        playAgainButton.onClick.AddListener(PlayAgainGame);

        canPlayAgain = true;
    }

    public override void OnStateExit()
    {
        scoreManager.ScoreReset();
        ammoLauncher.gameObject.SetActive(false);
        stateUI.SetActive(false);
    }

    private void ExitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void PlayAgainGame()
    {
        playAgainButton.gameObject.SetActive(false);
        scoreManager.ScoreReset();
        ammoLauncher.ammoCount = ammoLauncher.ammocap;
        ammoLauncher.SpawnAmmo();
        targetManager.DestroyAllTargets();
        targetManager.SpawnTargets(gameManager.gamePlane.gameObject);
    }
}
