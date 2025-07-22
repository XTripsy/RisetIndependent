using UnityEngine;

[CreateAssetMenu(fileName = "CookingBehavior", menuName = "ScriptableObject/CookingBehavior")]
public class CookingBehaviorSO : CookingBehaviorBase
{
    public Color cookedColor;
    public override void Cook(GameObject ingredient)
    {
        SpriteRenderer renderer = ingredient.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = cookedColor;
            Debug.Log($"{ingredient.name} Cooked and the color changed.");
        }
        else
        {
            Debug.LogWarning($"{ingredient.name} has no SpriteRenderer for cooking effect.");
        }
    }
}