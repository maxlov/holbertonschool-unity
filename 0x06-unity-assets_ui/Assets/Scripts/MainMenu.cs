using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameObject GameManager;
    private GameManager gameManager;

    private void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = GameManager.GetComponent<GameManager>();
    }

    public void LevelSelect(int level)
    {
        gameManager.Load("Level0" + level.ToString());
    }

    public void OptionsButton()
    {
        gameManager.Load("Options");
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
