using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 50f;

    void Update()
    {
        transform.Rotate(0f, Time.deltaTime * rotationSpeed, 0f);
    }
}
