using TMPro;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color blockedColor = Color.gray;
    
    private TextMeshPro _label;
    private Vector2Int _coordinates;
    private Waypoint _waypoint;

    private void Awake()
    {
        _waypoint = GetComponentInParent<Waypoint>();
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
        _label.color = _waypoint.IsPlaceable ? defaultColor : blockedColor;
    }

    private void DisplayCoordinates()
    {
        var position = transform.parent.position;
        _coordinates.x = Mathf.RoundToInt(position.x / EditorSnapSettings.move.x);
        _coordinates.y = Mathf.RoundToInt(position.z / EditorSnapSettings.move.z);
        _label.text = $"{_coordinates.x}.{_coordinates.y}";
    }

    private void UpdateObjectName()
    {
        transform.parent.name = _coordinates.ToString();
    }
}
