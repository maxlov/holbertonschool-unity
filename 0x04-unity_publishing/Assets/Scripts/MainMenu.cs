using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Material trapMat;
    public Material goalMat;
    public Toggle colorblindMode;

    private Color originalTrapColor;
    private Color originalGoalColor;

    private void Start()
    {
        originalTrapColor = trapMat.color;
        originalGoalColor = goalMat.color;
    }

    public void PlayMaze()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitMaze()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void settingColorBlindToggle()
    {
        if (colorblindMode.isOn)
        {
            trapMat.color = new Color32(255, 112, 0, 1);
            goalMat.color = Color.blue;
        }
        else
        {
            trapMat.color = originalTrapColor;
            goalMat.color = originalGoalColor;
        }
    }

    public void OnDestroy()
    {
        trapMat.color = originalTrapColor;
        goalMat.color = originalGoalColor;
    }
}
