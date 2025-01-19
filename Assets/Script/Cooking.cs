using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

enum eFireType
{
    Mati,
    Kecil,
    Sedang,
    Besar
}

enum eCookingType
{
    Fry,
    Grill,
    Boil
}

public class Recipe
{
    public string recipe_name;
    public List<string> ingredients;

    public Recipe(string recipeName, List<string> ingredient)
    {
        recipe_name = recipeName;
        ingredients = new List<string>(ingredient);
    }
}

public class Cooking : MonoBehaviour
{
    public List<Recipe> recipes = new List<Recipe>();
    List<string> current_ingredients = new List<string>();
    public GameObject base_menu;
    public List<Sprite> menus;

    int index_fire;
    bool bIsHover, bDoOnce;
    Collider2D collider2d;
    SpriteRenderer sprite_renderer;

    [SerializeField]
    eCookingType current_cooking_type;
    eFireType current_fire_type;

    [SerializeField]
    LayerMask layer_mask;
    [SerializeField]
    Transform holder;
    [SerializeField]
    List<Vector3> locations;
    [SerializeField]
    List<Vector3> has_locations;

    private void Awake()
    {
        collider2d = GetComponent<Collider2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        recipes.Add(new Recipe("AyamRicaRica", new List<string> { "Ayam", "Cabai", "BawangPutih", "BawangMerah" }));
        for (int i = 0; i < 5; i++)
        {
            current_ingredients.Add("");
        }
    }

    private void Update()
    {
        collider2d.callbackLayers = index_fire != 0 ? layer_mask : 0;

        CheckRecipe();
        IngredientsCheck();

        if (!bIsHover || holder.childCount > 0) return;

        index_fire += (int)Input.GetAxis("Mouse ScrollWheel");
        index_fire %= 4;

        switch (index_fire)
        {
            case -3:
                sprite_renderer.color = Color.yellow;
                current_fire_type = eFireType.Kecil;
                Debug.Log("Kecil");
                break;
            case -2:
                sprite_renderer.color = Color.red;
                current_fire_type = eFireType.Sedang;
                Debug.Log("Sedang");
                break;
            case -1:
                sprite_renderer.color = Color.blue;
                current_fire_type = eFireType.Besar;
                Debug.Log("Besar");
                break;
            case 0:
                sprite_renderer.color = Color.black;
                current_fire_type = eFireType.Mati;
                Debug.Log("Mati");
                break;
            case 1:
                sprite_renderer.color = Color.yellow;
                current_fire_type = eFireType.Kecil;
                Debug.Log("Kecil");
                break;
            case 2:
                sprite_renderer.color = Color.red;
                current_fire_type = eFireType.Sedang;
                Debug.Log("Sedang");
                break;
            case 3:
                sprite_renderer.color = Color.blue;
                current_fire_type = eFireType.Besar;
                Debug.Log("Besar");
                break;
        }
    }

    public bool IsRecipeMatch(List<string> inputIngredients, Recipe recipe)
    {
        var inputCount = new Dictionary<string, int>();
        foreach (var ingredient in inputIngredients)
        {
            if (inputCount.ContainsKey(ingredient))
                inputCount[ingredient]++;
            else
                inputCount[ingredient] = 1;
        }

        var recipeCount = new Dictionary<string, int>();
        foreach (var ingredient in recipe.ingredients)
        {
            if (recipeCount.ContainsKey(ingredient))
                recipeCount[ingredient]++;
            else
                recipeCount[ingredient] = 1;
        }

        foreach (var kvp in recipeCount)
        {
            if (!inputCount.ContainsKey(kvp.Key) || inputCount[kvp.Key] != kvp.Value)
                return false;
        }

        return true;
    }

    public void CheckRecipe()
    {
        if (IsRecipeMatch(current_ingredients, recipes[0]) && current_cooking_type == eCookingType.Fry)
        {
            ResetCooking();
            GameObject rica_ayam = Instantiate(base_menu);
            rica_ayam.transform.parent = holder;
            rica_ayam.transform.position = transform.position;
            rica_ayam.GetComponent<SpriteRenderer>().sprite = menus[0];
            rica_ayam.GetComponent<Food>().current_category = eCategory.Lauk;
            Color rica_color = Color.red;

            switch (current_fire_type)
            {
                case eFireType.Kecil:
                    rica_color *= 1;
                    rica_color.a = 1;
                    rica_ayam.GetComponent<SpriteRenderer>().color = rica_color;
                    Debug.LogWarning("RicaRica Belum Matang");
                    break;
                case eFireType.Sedang:
                    rica_color *= .5f;
                    rica_color.a = 1;
                    rica_ayam.GetComponent<SpriteRenderer>().color = rica_color;
                    Debug.LogWarning("RicaRica Matang");
                    break;
                case eFireType.Besar:
                    rica_color *= .1f;
                    rica_color.a = 1;
                    rica_ayam.GetComponent<SpriteRenderer>().color = rica_color;
                    Debug.LogWarning("RicaRica Gosong");
                    break;
            }
        }
    }

    void ResetCooking()
    {
        foreach (Transform child in holder.transform)
        {
            Destroy(child.gameObject);
        }
        current_ingredients.Clear();
        sprite_renderer.color = Color.black;
        index_fire = 0;
    }

    void IngredientsCheck()
    {
        for (int i = 0; i < current_ingredients.Count; i++)
        {
            if (i > holder.childCount - 1)
                current_ingredients[i] = "";
        }

        for (int i = 0; i < holder.childCount; i++)
        {
            if (holder.GetChild(i).GetComponent<Ingredient>() == null) return;

            eIngredient ingredient = holder.GetChild(i).GetComponent<Ingredient>().current_ingredient_type;

            switch (ingredient)
            {
                case eIngredient.Ayam:
                    current_ingredients[i] = "Ayam";
                    break;
                case eIngredient.Telur:
                    current_ingredients[i] = "Telur";
                    break;
                case eIngredient.Wortel:
                    current_ingredients[i] = "Wortel";
                    break;
                case eIngredient.Cabai:
                    current_ingredients[i] = "Cabai";
                    break;
                case eIngredient.BawangPutih:
                    current_ingredients[i] = "BawangPutih";
                    break;
                case eIngredient.BawangMerah:
                    current_ingredients[i] = "BawangMerah";
                    break;
                default:
                    current_ingredients[i] = "";
                    break;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (holder.childCount == 0)
            bIsHover = true;
    }

    private void OnMouseExit()
    {
        if (holder.childCount == 0)
            bIsHover = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (holder.childCount > 4) return;

        if (other.gameObject.tag == "Ingredient")
        {
            IInterfaceIngredient IIngredient = other.gameObject.GetComponent<IInterfaceIngredient>() as IInterfaceIngredient;
            IIngredient.ISpawn(true);
            IInterfaceDragDrop IDragDrop = other.gameObject.GetComponent<IInterfaceDragDrop>() as IInterfaceDragDrop;
            has_locations.Add(locations[0]);
            locations.RemoveAt(0);
            IDragDrop.ITriggerEnter(has_locations[has_locations.Count-1]);
            IDragDrop.ITriggerParent(holder);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ingredient")
        {
            IInterfaceIngredient IIngredient = other.gameObject.GetComponent<IInterfaceIngredient>() as IInterfaceIngredient;
            IIngredient.ISpawn(false);
            IInterfaceDragDrop IDragDrop = other.gameObject.GetComponent<IInterfaceDragDrop>() as IInterfaceDragDrop;
            IDragDrop.ITriggerExit();
            IDragDrop.ITriggerParent(transform.parent);

            for (int i = 0; i < holder.childCount; i++)
            {
                if (holder.GetChild(i).position != has_locations[i])
                {
                    locations.Add(has_locations[i]);
                    has_locations.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
