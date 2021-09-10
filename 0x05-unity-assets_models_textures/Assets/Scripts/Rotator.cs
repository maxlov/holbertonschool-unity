using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float RotateSpeed = 25.0f;
    public int x = 0;
    public int y = 0;
    public int z = 0;
    
    void Update()
    {
        Vector3 rotationVector = new Vector3(x, y, z);
        transform.Rotate(rotationVector * RotateSpeed * Time.deltaTime);    
    }
}
