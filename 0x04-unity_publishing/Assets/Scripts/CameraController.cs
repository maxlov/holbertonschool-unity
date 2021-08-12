using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public float offsetX = -1;
    public float offsetZ = -9;
    float x;
    float z;

    private void LateUpdate()
    {
        x = player.transform.position.x + offsetX;
        z = player.transform.position.z + offsetZ;
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
