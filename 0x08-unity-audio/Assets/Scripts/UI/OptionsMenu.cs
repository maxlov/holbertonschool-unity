using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private GameObject GameManager;
    private GameManager gameManager;

    public Toggle InvertY;

    private int InvertYTemp;
    private float SFXVolumeTemp;
    private float BGMVolumeTemp;

    private void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = GameManager.GetComponent<GameManager>();

        InvertY.isOn = PlayerPrefs.GetInt("InvertedY") == 1 ? true : false;
        InvertYTemp = PlayerPrefs.GetInt("InvertedY");
    }

    public void Back()
    {
        gameManager.LoadPrev();
    }

    public void Apply()
    {
        PlayerPrefs.SetInt("InvertedY", InvertYTemp);
        gameManager.LoadPrev();
    }

    public void InvertYToggle()
    {
        InvertYTemp = InvertY.isOn ? 1 : 0;
    }
}
