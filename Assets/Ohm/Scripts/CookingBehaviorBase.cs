using UnityEngine;

public abstract class CookingBehaviorBase : ScriptableObject
{
    [SerializeField] private CookingType cookingType;
    [SerializeField, Range(0f, 10f)] private float cookTime = 2f;

    public CookingType CookingType => cookingType;
    public float CookTime => cookTime;

    public abstract void Cook(GameObject ingredient);
}