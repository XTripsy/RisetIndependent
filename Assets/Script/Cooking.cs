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
    Transform holder_ingridient;
    [SerializeField]
    Transform holder_food;
    [SerializeField]
    List<Vector3> locations;
    [SerializeField]
    List<Vector3> has_locations;

    private void Awake()
    {
        collider2d = GetComponent<Collider2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        recipes.Add(new Recipe("AyamRicaRica", new List<string> { "Ayam", "Cabai", "BawangPutih", "BawangMerah" }));
        recipes.Add(new Recipe("TelurMalaka", new List<string> { "Telur", "Cabai", "BawangMerah" }));
        for (int i = 0; i < 5; i++)
        {
            current_ingredients.Add("NULL");
        }
    }

    private void Update()
    {
        collider2d.callbackLayers = index_fire != 0 ? layer_mask : 0;

        for (int i = 0; i < current_ingredients.Count; i++)
        {
            Debug.Log(current_ingredients[i]);
        }

        if (holder_food.childCount > 0)
        {
            ResetCooking();
            return;
        }

        if (holder_ingridient.childCount > 0)
        {
            CheckRecipe();
            IngredientsCheck();
        }

        if (!bIsHover || holder_ingridient.childCount > 0) return;

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
            MakeFood("AyamRica", eCategory.Lauk, 0);

        if (IsRecipeMatch(current_ingredients, recipes[1]) && current_cooking_type == eCookingType.Fry)
            MakeFood("TelurMalaka", eCategory.Sup, 1);
    }

    void MakeFood(string name_menu, eCategory category_menu, int index)
    {
        ResetCooking();
        GameObject menu = Instantiate(base_menu);
        menu.transform.parent = holder_food;
        menu.transform.position = transform.position;
        menu.GetComponent<SpriteRenderer>().sprite = menus[index];
        menu.GetComponent<Food>().current_category = category_menu;
        menu.GetComponent<Food>().name_menu = name_menu;
        Color menu_color = Color.red;

        switch (current_fire_type)
        {
            case eFireType.Kecil:
                menu_color *= 1;
                menu_color.a = 1;
                menu.GetComponent<SpriteRenderer>().color = menu_color;
                menu.GetComponent<Food>().maturity_level = 1;
                Debug.LogWarning(name_menu + "Belum Matang");
                break;
            case eFireType.Sedang:
                menu_color *= .5f;
                menu_color.a = 1;
                menu.GetComponent<SpriteRenderer>().color = menu_color;
                menu.GetComponent<Food>().maturity_level = 2;
                Debug.LogWarning(name_menu + "Matang");
                break;
            case eFireType.Besar:
                menu_color *= .1f;
                menu_color.a = 1;
                menu.GetComponent<SpriteRenderer>().color = menu_color;
                menu.GetComponent<Food>().maturity_level = 3;
                Debug.LogWarning(name_menu + "Gosong");
                break;
        }
    }

    void ResetCooking()
    {
        foreach (Transform child in holder_ingridient.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < has_locations.Count; i++)
        {
            locations.Add(has_locations[i]);
        }

        has_locations.Clear();

        for (int i = 0; i < 5; i++)
        {
            current_ingredients[i] = "NULL";
        }

        sprite_renderer.color = Color.black;
        index_fire = 0;
    }

    void IngredientsCheck()
    {
        for (int i = 0; i < holder_ingridient.childCount; i++)
        {
            if (holder_ingridient.GetChild(i).GetComponent<Ingredient>() == null) return;

            eIngredient ingredient = holder_ingridient.GetChild(i).GetComponent<Ingredient>().current_ingredient_type;

            Debug.LogWarning(holder_ingridient.childCount);

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
                    current_ingredients[i] = "NULL";
                    break;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (holder_ingridient.childCount == 0)
            bIsHover = true;
    }

    private void OnMouseExit()
    {
        if (holder_ingridient.childCount == 0)
            bIsHover = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (holder_ingridient.childCount > 4) return;

        if (other.gameObject.tag == "Ingredient")
        {
            IInterfaceIngredient IIngredient = other.gameObject.GetComponent<IInterfaceIngredient>() as IInterfaceIngredient;
            IIngredient.ISpawn(true);
            IInterfaceDragDrop IDragDrop = other.gameObject.GetComponent<IInterfaceDragDrop>() as IInterfaceDragDrop;
            has_locations.Add(locations[0]);
            locations.RemoveAt(0);
            IDragDrop.ITriggerEnter(has_locations[has_locations.Count-1]);
            IDragDrop.ITriggerParent(holder_ingridient);
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

            for (int i = 0; i < holder_ingridient.childCount; i++)
            {
                if (holder_ingridient.GetChild(i).position != has_locations[i])
                {
                    locations.Add(has_locations[i]);
                    has_locations.RemoveAt(i);
                    current_ingredients[i] = "NULL";
                    return;
                }
            }
        }
    }
}
