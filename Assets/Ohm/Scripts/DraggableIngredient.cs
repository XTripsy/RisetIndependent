using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableIngredient : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum DropBehavior { SnapBack, Destroy }
    [SerializeField] private DropBehavior dropBehavior = DropBehavior.SnapBack;
    [SerializeField] private string validDropTag = "DropZone";
    [SerializeField] private LayerMask interactorLayer;
    [SerializeField, Range(0f, 50f)] private float dragSmoothing = 25f; // Higher = snappier, lower = smoother

    private Vector3 originalPosition;
    private Camera mainCamera;
    private bool isDragging;
    private Vector3 targetPosition;
    public delegate void IngredientDroppedHandler(GameObject ingredient, bool wasDestroyed);
    public static event IngredientDroppedHandler OnIngredientDropped;

    private void Awake() {
        interactorLayer = LayerMask.GetMask("Interactor");
    }

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found! Ensure the camera is tagged 'MainCamera'.");
            return;
        }
        if (!mainCamera.GetComponent<Physics2DRaycaster>())
        {
            Debug.LogWarning("No Physics2DRaycaster on Main Camera! Adding one for 2D sprite interaction.");
            mainCamera.gameObject.AddComponent<Physics2DRaycaster>();
        }
        originalPosition = transform.position;
        targetPosition = transform.position;
        Debug.Log($"{name} initialized at {originalPosition}");
    }

    void Update()
    {
        if (isDragging && mainCamera != null)
        {
            // Smoothly interpolate to target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, dragSmoothing * Time.deltaTime);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!mainCamera) return;
        isDragging = true;
        Debug.Log($"{name} drag started");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!mainCamera || !isDragging) return;
        targetPosition = mainCamera.ScreenToWorldPoint(eventData.position);
        targetPosition.z = transform.position.z; // Maintain sprite's z-position
        // Debug.Log($"{name} dragging to {targetPosition}");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!mainCamera) return;
        isDragging = false;
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(eventData.position), Vector2.zero, Mathf.Infinity, interactorLayer);
        Debug.Log(hit.collider);
        bool validDrop = hit.collider != null && hit.collider.CompareTag(validDropTag);
        Debug.Log($"{name} dropped. Valid zone: {validDrop}, Hit: {(hit.collider ? hit.collider.name : "none")}");

        if (validDrop)
        {
            if (dropBehavior == DropBehavior.Destroy)
            {
                OnIngredientDropped?.Invoke(gameObject, true);
                Debug.Log($"{name} destroyed in valid drop zone");
                Destroy(gameObject);
            }
            else
            {
                transform.position = hit.collider.transform.position;
                originalPosition = transform.position;
                transform.parent = hit.collider.transform;
                targetPosition = transform.position; // Sync target to avoid drift
                OnIngredientDropped?.Invoke(gameObject, false);
                Debug.Log($"{name} placed in drop zone at {transform.position}");
            }
        }
        else if (dropBehavior == DropBehavior.SnapBack)
        {
            transform.position = originalPosition;
            targetPosition = originalPosition; // Sync target to avoid drift
            OnIngredientDropped?.Invoke(gameObject, false);
            Debug.Log($"{name} snapped back to {originalPosition}");
        }
    }

    
}