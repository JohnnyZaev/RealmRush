using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Pathfinder : MonoBehaviour
    {
        [SerializeField] private Vector2Int startCoordinates;
        public Vector2Int StartCoordinates
        {
            get { return startCoordinates; }
        }
        
        [SerializeField] private Vector2Int endCoordinates;
        public Vector2Int EndCoordinates
        {
            get { return endCoordinates; }
        }

        private Node _startingNode;
        private Node _destinationNode;
        [SerializeField] private Node currentSearchNode;

        private Queue<Node> frontier = new Queue<Node>();
        private Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

        private Vector2Int[] directions = {
            Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down
        };

        private GridManager gridManager;
        private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

        private void Awake()
        {

            gridManager = FindObjectOfType<GridManager>();
            if (gridManager != null)
            {
                grid = gridManager.Grid;
                _startingNode = grid[startCoordinates];
                _destinationNode = grid[endCoordinates];
            }
        }

        private void Start()
        {
            GetNewPath();
        }

        public List<Node> GetNewPath()
        {
            gridManager.ResetNodes();
            BreadthFirstSearch();
            return BuildPath();
        }

        private void ExploreNeighbors()
        {
            List <Node> neighbors = new List<Node>();

            foreach (var direction in directions)
            {
                Vector2Int neighborCoord = currentSearchNode.coordinates + direction;

                if (grid.ContainsKey(neighborCoord))
                {
                    neighbors.Add(grid[neighborCoord]);
                }

                foreach (var neighbor in neighbors)
                {
                    if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
                    {
                        neighbor.connectedTo = currentSearchNode;
                        reached.Add(neighbor.coordinates, neighbor);
                        frontier.Enqueue(neighbor);
                    }
                }
            }
        }

        private void BreadthFirstSearch()
        {
            
            _startingNode.isWalkable = true;
            _destinationNode.isWalkable = true;
            
            frontier.Clear();
            reached.Clear();
            
            bool isRunning = true;
            frontier.Enqueue(_startingNode);
            reached.Add(startCoordinates, _startingNode);

            while (frontier.Count > 0 && isRunning)
            {
                currentSearchNode = frontier.Dequeue();
                currentSearchNode.isExplored = true;
                ExploreNeighbors();
                if (currentSearchNode.coordinates == _destinationNode.coordinates)
                {
                    isRunning = false;
                }
            }
        }

        private List<Node> BuildPath()
        {
            List<Node> path = new List<Node>();
            Node currentNode = _destinationNode;
            
            path.Add(currentNode);
            currentNode.isPath = true;

            while (currentNode.connectedTo != null)
            {
                currentNode = currentNode.connectedTo;
                path.Add(currentNode);
                currentNode.isPath = true;
            }
            
            path.Reverse();
            return path;
        }

        public bool WillBlockPath(Vector2Int coordinates)
        {
            if (grid.ContainsKey(coordinates))
            {
                bool previousState = grid[coordinates].isWalkable;
                grid[coordinates].isWalkable = false;
                var newPath = BuildPath();
                grid[coordinates].isWalkable = previousState;

                if (newPath.Count <= 1)
                {
                    GetNewPath();
                    return true;
                }
            }

            return false;
        }

        public void NotifyReceivers()
        {
            BroadcastMessage("RecalculatePath", SendMessageOptions.DontRequireReceiver);
        }
    }
}
