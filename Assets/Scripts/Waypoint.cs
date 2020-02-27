using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Waypoint.png", true);
    }
}
