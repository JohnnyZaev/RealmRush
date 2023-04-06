using UnityEngine;

namespace Pathfinding
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Node node;

        private void Start()
        {
            Debug.Log(node.coordinates);
            Debug.Log(node.isWalkable);
        }
    }
}
