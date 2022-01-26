using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlaneNavBuild : MonoBehaviour
{
    void Awake()
    {
        gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
