using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    private GameObject player;
    private Timer timerCode;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timerCode = player.GetComponent<Timer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timerCode.Win();
        }
    }
}
