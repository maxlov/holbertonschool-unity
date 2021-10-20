using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            player.GetComponent<Timer>().enabled = true;
    }
}
