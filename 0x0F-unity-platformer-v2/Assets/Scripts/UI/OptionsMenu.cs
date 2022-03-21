using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    private GameObject GameManager;
    private GameManager gameManager;

    public Toggle InvertY;
    public Slider SFXVol;
    public Slider BGMVol;

    private int InvertYTemp;
    private float SFXVolumeTemp;
    private float BGMVolumeTemp;

    public AudioMixer masterMixer;

    private void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = GameManager.GetComponent<GameManager>();

        InvertY.isOn = PlayerPrefs.GetInt("InvertedY") == 1 ? true : false;
        InvertYTemp = PlayerPrefs.GetInt("InvertedY");

        SFXVol.value = Mathf.Pow(10.0f, PlayerPrefs.GetFloat("SFXvol", 0.0f) / 20.0f);
        BGMVol.value = Mathf.Pow(10.0f, PlayerPrefs.GetFloat("BGMvol", 0.0f) / 20.0f);
    }

    public void Back()
    {
        gameManager.Load("MainMenu");
    }

    public void Apply()
    {
        PlayerPrefs.SetInt("InvertedY", InvertYTemp);
        PlayerPrefs.SetFloat("BGMvol", BGMVolumeTemp);
        PlayerPrefs.SetFloat("SFXvol", SFXVolumeTemp);
        gameManager.LoadPrev();
    }

    public void Rebind()
    {
        gameManager.Load("OptionsKeyBinding");
    }

    public void InvertYToggle()
    {
        InvertYTemp = InvertY.isOn ? 1 : 0;
    }

    public void SetMusicVol()
    {
        BGMVolumeTemp = Mathf.Log10(BGMVol.value) * 20;
        Debug.Log($"slidVal {BGMVol.value} BGMtempVol {BGMVolumeTemp}");
    }

    public void SetSFXVol()
    {
        SFXVolumeTemp = Mathf.Log10(SFXVol.value) * 20;
    }


}
