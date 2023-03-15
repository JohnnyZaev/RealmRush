using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Waypoint> waypoints = new List<Waypoint>();

    private void Start()
    {
        foreach (var waypoint in waypoints)
        {
            Debug.Log(waypoint.name);
        }
    }
}
