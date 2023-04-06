using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridSize;
        
        private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

        private void Awake()
        {
            CreateGrid();
        }

        public Node GetNode(Vector2Int coordinates)
        {
            if (!grid.ContainsKey(coordinates))
                return null;
            return grid[coordinates];
        }

        private void CreateGrid()
        {
            for (var x = 0; x < gridSize.x; x++)
            {
                for (var y = 0; y < gridSize.y; y++)
                {
                    var coordinates = new Vector2Int(x, y);
                    grid.Add(coordinates, new Node(coordinates, true));
                    Debug.Log(grid[coordinates].coordinates + "=" + grid[coordinates].isWalkable);
                }
            }
        }
    }
}
