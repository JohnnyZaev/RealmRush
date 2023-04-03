using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Waypoint> path = new();
    [SerializeField] [Range(0f, 5f)] private float speed = 2f;
    
    private WaitForEndOfFrame _pathWaitTime;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _pathWaitTime = new WaitForEndOfFrame();
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void FindPath()
    {
        path.Clear();
        
        var waypoints = GameObject.FindGameObjectsWithTag("Path");

        foreach (var waypoint in waypoints)
        {
            path.Add(waypoint.GetComponent<Waypoint>());
        }
    }

    private void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    private IEnumerator FollowPath()
    {
        foreach (var waypoint in path)
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
        _enemy.StealGold();
        gameObject.SetActive(false);
    }
}
