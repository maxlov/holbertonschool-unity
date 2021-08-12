using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 50f;

    void Update()
    {
        transform.Rotate(Time.deltaTime * rotationSpeed, 0f, 0f);
    }
}
