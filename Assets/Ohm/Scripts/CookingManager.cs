using UnityEngine;
using System.Collections;

public enum CookingType
{
    Boil,
    Grill,
    Fry,
    Burn
}

public class CookingManager : MonoBehaviour
{
    [SerializeField] private CookingType cookingType;
    [SerializeField] private CookingBehaviorBase cookingBehavior;

    private void OnEnable()
    {
        DraggableIngredient.OnIngredientDropped += HandleIngredientDropped;
    }

    private void OnDisable()
    {
        DraggableIngredient.OnIngredientDropped -= HandleIngredientDropped;
    }

    private void HandleIngredientDropped(GameObject ingredient, bool wasDestroyed)
    {
        if (wasDestroyed) return; // Ignore if ingredient was destroyed

        // Check if the ingredient was dropped on this cookingware
        Vector2 dropPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(dropPoint, Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            if (cookingBehavior != null && cookingBehavior.CookingType == cookingType)
            {
                StartCoroutine(CookIngredient(ingredient));
                Debug.Log($"{ingredient.name} dropped on {gameObject.name} for {cookingType} cooking.");
            }
            else
            {
                Debug.LogWarning($"CookingBehaviorBase type mismatch or not assigned for {gameObject.name}.");
            }
        }
    }

    private IEnumerator CookIngredient(GameObject ingredient)
    {
        yield return new WaitForSeconds(cookingBehavior.CookTime);
        cookingBehavior.Cook(ingredient);
    }
}