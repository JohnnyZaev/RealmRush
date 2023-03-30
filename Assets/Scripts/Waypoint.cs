using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Tower tower;
    [SerializeField] private bool isPlaceable;

    public bool IsPlaceable => isPlaceable;

    private void OnMouseDown()
    {
        if (!isPlaceable) return;
        var isPlaced = tower.CreateTower(tower, transform.position);
        // Instantiate(tower, transform.position, Quaternion.identity);
        isPlaceable = !isPlaced;
    }
}
