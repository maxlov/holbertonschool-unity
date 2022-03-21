using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameObject GameManager;
    private GameManager gameManager;
    private InputManager inputManager;

    private void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = GameManager.GetComponent<GameManager>();
        inputManager = GetComponent<InputManager>();
        if (inputManager != null)
            inputManager.LoadAllBindingOverrides();
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
