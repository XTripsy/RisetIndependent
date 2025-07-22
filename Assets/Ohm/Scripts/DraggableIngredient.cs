using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableIngredient : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum DropBehavior { SnapBack, Destroy }
    [SerializeField] private DropBehavior dropBehavior = DropBehavior.SnapBack;
    [SerializeField] private string validDropTag = "DropZone";

    private Vector3 originalPosition;
    private Camera mainCamera;
    private bool isDragging;
    public delegate void IngredientDroppedHandler(GameObject ingredient, bool wasDestroyed);
    public static event IngredientDroppedHandler OnIngredientDropped;

    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Maintain z-position
        transform.position = mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        // Check for valid drop zone
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        bool validDrop = hit.collider != null && hit.collider.CompareTag(validDropTag);

        if (validDrop)
        {
            if (dropBehavior == DropBehavior.Destroy)
            {
                OnIngredientDropped?.Invoke(gameObject, true);
                Destroy(gameObject);
            }
            else
            {
                // Optionally place in drop zone
                transform.position = hit.collider.transform.position;
                OnIngredientDropped?.Invoke(gameObject, false);
            }
        }
        else if (dropBehavior == DropBehavior.SnapBack)
        {
            transform.position = originalPosition;
            OnIngredientDropped?.Invoke(gameObject, false);
        }
    }
}