using Pathfinding;
using TMPro;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color blockedColor = Color.gray;
    [SerializeField] private Color exploredColor = Color.yellow;
    [SerializeField] private Color pathColor = new Color(1f, 0.5f, 0f);
    
    private TextMeshPro _label;
    private Vector2Int _coordinates;
    private GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        _label = GetComponent<TextMeshPro>();
        _label.enabled = true;
        DisplayCoordinates();
    }

    private void Update()
    {
        ColorCoordinates();
        ToggleLabels();
        if (Application.isPlaying)
            return;
        DisplayCoordinates();
        UpdateObjectName();
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _label.enabled = !_label.IsActive();
        }
    }

    private void ColorCoordinates()
    {
        if (gridManager == null)
            return;

        var node = gridManager.GetNode(_coordinates);

        if (node == null)
            return;
        
        if (!node.isWalkable)
            _label.color = blockedColor;
        else if (node.isPath)
            _label.color = pathColor;
        else if (node.isExplored)
            _label.color = exploredColor;
        else
            _label.color = defaultColor;
    }

    private void DisplayCoordinates()
    {
        if (gridManager == null)
            return;
        var position = transform.parent.position;
        _coordinates.x = Mathf.RoundToInt(position.x / gridManager.UnityGridSize);
        _coordinates.y = Mathf.RoundToInt(position.z / gridManager.UnityGridSize);
        _label.text = $"{_coordinates.x}.{_coordinates.y}";
    }

    private void UpdateObjectName()
    {
        transform.parent.name = _coordinates.ToString();
    }
}
