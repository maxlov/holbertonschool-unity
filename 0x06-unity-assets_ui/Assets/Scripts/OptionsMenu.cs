using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    private GameObject GameManager;
    private GameManager gameManager;

    private void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = GameManager.GetComponent<GameManager>();
    }

    public void Back()
    {
        gameManager.LoadPrev();
    }
}
