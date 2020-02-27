using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyPathing : MonoBehaviour
{
    public void Awake()
    {
        var agent = GetComponent<NavMeshAgent>();
        var destination = GameObject.FindGameObjectWithTag("Waypoint");
        if (destination != null)
            agent.SetDestination(destination.transform.position);
        else
            Debug.LogWarning($"[ Enemy Pathing ] Unable to find waypoint to move to.");
    }
}
