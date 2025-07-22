using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CrossAutoLayout : MonoBehaviour
{
    [SerializeField] private Vector2 topOffset = new Vector2(0f, 1f);
    [SerializeField] private Vector2 bottomOffset = new Vector2(0f, -1f);
    [SerializeField] private Vector2 leftOffset = new Vector2(-1f, 0f);
    [SerializeField] private Vector2 rightOffset = new Vector2(1f, 0f);
    [SerializeField, Range(1, 5)] private int maxChildren = 5;

    private List<Transform> children = new List<Transform>();

    void OnTransformChildrenChanged()
    {
        UpdateChildList();
        AlignChildren();
    }

    void Start()
    {
        UpdateChildList();
        AlignChildren();
    }

    private void UpdateChildList()
    {
        // Get current children, excluding inactive or non-direct children
        children = transform.Cast<Transform>()
            .Where(child => child.gameObject.activeSelf)
            .Take(maxChildren)
            .ToList();
        Debug.Log($"CrossLayoutManager: Updated child list with {children.Count} children.");
    }

    private void AlignChildren()
    {
        for (int i = 0; i < children.Count; i++)
        {
            Vector3 newPosition = transform.position; // Center by default
            switch (i)
            {
                case 0: // Center
                    break;
                case 1: // Top
                    newPosition += (Vector3)topOffset;
                    break;
                case 2: // Bottom
                    newPosition += (Vector3)bottomOffset;
                    break;
                case 3: // Left
                    newPosition += (Vector3)leftOffset;
                    break;
                case 4: // Right
                    newPosition += (Vector3)rightOffset;
                    break;
            }
            // Preserve child's z-position
            newPosition.z = children[i].position.z;
            children[i].position = newPosition;
            Debug.Log($"CrossLayoutManager: Positioned {children[i].name} at {newPosition}");
        }
    }

    // Public method to manually trigger realignment (e.g., for runtime additions)
    public void RealignChildren()
    {
        UpdateChildList();
        AlignChildren();
    }
}