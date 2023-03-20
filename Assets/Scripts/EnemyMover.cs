using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Waypoint> waypoints = new();
    [SerializeField] [Range(0f, 5f)] private float speed = 2f;
    
    private WaitForEndOfFrame _pathWaitTime;

    private void Start()
    {
        _pathWaitTime = new WaitForEndOfFrame();
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        foreach (var waypoint in waypoints)
        {
            var starterPosition = transform.position;
            var endPosition = waypoint.transform.position;
            var travelPercent = 0f;

            transform.LookAt(endPosition);
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(starterPosition, endPosition, travelPercent);
                yield return _pathWaitTime;
            }
        }
    }
}
