using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] private float speed = 2f;
    
    private List<Node> path = new();
    private WaitForEndOfFrame _pathWaitTime;
    private Enemy _enemy;
    private GridManager gridManager;
    private Pathfinder pathfinder;

    
    
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _pathWaitTime = new WaitForEndOfFrame();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    private void OnEnable()
    {
        RecalculatePath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void RecalculatePath()
    {
        path.Clear();

        path = pathfinder.GetNewPath();
    }

    private void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    private void FinishPath()
    {
        _enemy.StealGold();
        gameObject.SetActive(false);
    }

    private IEnumerator FollowPath()
    {
        for (var index = 1; index < path.Count; index++)
        {
            var waypoint = path[index];
            var starterPosition = transform.position;
            var endPosition = gridManager.GetPositionFromCoordinates(waypoint.coordinates);
            var travelPercent = 0f;

            transform.LookAt(endPosition);
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(starterPosition, endPosition, travelPercent);
                yield return _pathWaitTime;
            }
        }

        FinishPath();
    }
}
