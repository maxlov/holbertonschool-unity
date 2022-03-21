using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    private GameObject GameManager;
    private GameManager gameManager;

    public GameObject PauseCanvas;

    private void Awake()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = GameManager.GetComponent<GameManager>();
    }

    public void MenuButton()
    {
        gameManager.Load("MainMenu");
    }

    public void Next()
    {
        gameManager.LoadNext();
    }
}
