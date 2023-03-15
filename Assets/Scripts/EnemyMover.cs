using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Waypoint> waypoints = new List<Waypoint>();
    [SerializeField] private float waitTime = 1f;
    
    private WaitForSecondsRealtime _pathWaitTime;

    private void Start()
    {
        _pathWaitTime = new WaitForSecondsRealtime(waitTime);
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        foreach (var waypoint in waypoints)
        {
            transform.position = waypoint.transform.position;
            yield return _pathWaitTime;
        }
    }
}
