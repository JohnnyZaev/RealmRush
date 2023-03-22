using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private GameObject tower;
    [SerializeField] private bool isPlaceable;

    public bool IsPlaceable => isPlaceable;

    private void OnMouseDown()
    {
        if (!isPlaceable) return;
        Instantiate(tower, transform.position, Quaternion.identity);
        isPlaceable = false;
    }
}
