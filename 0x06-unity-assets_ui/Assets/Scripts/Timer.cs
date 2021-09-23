using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float time = 0f;

    public GameObject winCanvas;
    public Text winTimerText;

    void Update()
    {
        time += Time.deltaTime;

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = time % 60;

        timerText.text = $"{minutes.ToString()}:{seconds.ToString("0#.00")}";
    }

    public void Win()
    {
        winCanvas.SetActive(true);
        winTimerText.text = timerText.text;
        timerText.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
