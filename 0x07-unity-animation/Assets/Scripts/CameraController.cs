using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public GameObject vcam;

    public bool isInverted;

    void Start()
    {
        CinemachineFreeLook freeLook = vcam.GetComponent<CinemachineFreeLook>();

        if (PlayerPrefs.GetInt("InvertedY") == 1)
            freeLook.m_YAxis.m_InvertInput = true;
        else
            freeLook.m_YAxis.m_InvertInput = false;
    }
}
