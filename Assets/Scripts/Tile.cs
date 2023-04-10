using Pathfinding;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Tower tower;
    [SerializeField] private bool isPlaceable;

    public bool IsPlaceable => isPlaceable;

    private GridManager gridManager;
    private Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (!isPlaceable) return;
        var isPlaced = tower.CreateTower(tower, transform.position);
        isPlaceable = !isPlaced;
    }
}
