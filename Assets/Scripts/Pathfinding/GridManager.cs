using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private int unityGridSize = 10;
        public int UnityGridSize
        {
            get { return unityGridSize; }
        }
        
        private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
        public Dictionary<Vector2Int, Node> Grid
        {
            get { return grid; }
        }

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

        public void BlockNode(Vector2Int coordinates)
        {
            if (grid.ContainsKey(coordinates))
            {
                grid[coordinates].isWalkable = false;
            }
        }

        public Vector2Int GetCoordinatesFromPosition(Vector3 position)
        {
            Vector2Int coordinates = new Vector2Int();
            coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
            coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

            return coordinates;
        }
        
        public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
        {
            Vector3 position = new Vector3();
            position.x = coordinates.x * unityGridSize;
            position.z = coordinates.y * unityGridSize;

            return position;
        }

        private void CreateGrid()
        {
            for (var x = 0; x < gridSize.x; x++)
            {
                for (var y = 0; y < gridSize.y; y++)
                {
                    var coordinates = new Vector2Int(x, y);
                    grid.Add(coordinates, new Node(coordinates, true));
                }
            }
        }
    }
}
