using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetAI : MonoBehaviour
{
    private Vector3 rngDirection;
    private NavMeshAgent agent;
    [SerializeField] private int walkRadius;

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!agent.hasPath)
            RandomizeDestination();
    }

    private void RandomizeDestination()
    {
        if (!agent.enabled)
            agent.enabled = true;
        rngDirection = Random.insideUnitSphere * walkRadius;
        rngDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(rngDirection, out hit, walkRadius, 1);
        agent.destination = hit.position;
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        NavMeshAgent debug_agent = gameObject.GetComponent<NavMeshAgent>();
        if (debug_agent.enabled)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, debug_agent.destination - transform.position);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(debug_agent.destination, .01f);
        }
    }
#endif
}
