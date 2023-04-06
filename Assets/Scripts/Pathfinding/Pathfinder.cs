using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Pathfinder : MonoBehaviour
    {
        [SerializeField] private Node currentGridNode;
        private Vector2Int[] directions = {
            Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down
        };

        private GridManager gridManager;
        private Dictionary<Vector2Int, Node> grid;

        private void Awake()
        {
            gridManager = FindObjectOfType<GridManager>();
            if (gridManager != null)
            {
                grid = gridManager.Grid;
            }
        }

        private void Start()
        {
            ExploreNeighbors();
        }

        private void ExploreNeighbors()
        {
            List <Node> neighbors = new List<Node>();

            foreach (var direction in directions)
            {
                Vector2Int neighborCoord = currentGridNode.coordinates + direction;

                if (grid.ContainsKey(neighborCoord))
                {
                    neighbors.Add(grid[neighborCoord]);
                    
                    //TODO: remove after testing
                    grid[neighborCoord].isExplored = true;
                    grid[currentGridNode.coordinates].isPath = true;
                }
            }
        }
    }
}
