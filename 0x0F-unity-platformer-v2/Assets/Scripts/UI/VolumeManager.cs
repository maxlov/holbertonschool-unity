using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class VolumeManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    public static VolumeManager instance;

    /*void Awake()
    {
        if (VolumeManager.instance == null)
        {
            DontDestroyOnLoad(gameObject);
            VolumeManager.instance = this;
        }
        else
            Destroy(gameObject);
    }*/
    void Start()
    {
        //Get the saved music volume, standard = 10f
        float music = PlayerPrefs.GetFloat("BGMvol", 0.0f);
        float master = PlayerPrefs.GetFloat("SFXvol", 0.0f);
        //Set the music volume to the saved volume
        AdjustMusicVolume(music);
        AdjustSFXVolume(master);
    }



    public void AdjustMusicVolume(float volume)
    {
        //Update AudioMixer
        audioMixer.SetFloat("BGMvol", volume);
        //Update PlayerPrefs "Music"
        PlayerPrefs.SetFloat("BGMvol", volume);
        //Save changes
        PlayerPrefs.Save();
    }

    public void AdjustSFXVolume(float volume)
    {
        //Update AudioMixer
        audioMixer.SetFloat("Mastervol", volume);
        //Update PlayerPrefs "Music"
        PlayerPrefs.SetFloat("SFXvol", volume);
        //Save changes
        PlayerPrefs.Save();
    }

}
