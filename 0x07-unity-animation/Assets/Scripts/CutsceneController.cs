using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject TimerCanvas;
    public PlayerController PlayerScript;

    public void ExitCutscene()
    {
        MainCamera.SetActive(true);
        PlayerScript.enabled = true;
        TimerCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
