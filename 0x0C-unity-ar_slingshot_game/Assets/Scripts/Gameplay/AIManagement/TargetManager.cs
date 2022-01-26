using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject target_prefab;
    public GameObject target_parent_prefab;
    public int target_count = 5;

    [HideInInspector]
    public List<GameObject> target_list = new List<GameObject>();
    [HideInInspector]
    public GameObject target_parent;

    public void SpawnTargets(GameObject plane)
    {
        if (target_parent == null)
            target_parent = Instantiate(target_parent_prefab, plane.transform.position, Quaternion.identity, plane.transform);
        for (int i = 0; i < target_count; i++)
            target_list.Add(Instantiate(target_prefab, target_parent.transform.position, Quaternion.identity, target_parent.transform));
    }
}
