using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject Recipe;
    void OnMouseDown()
    {
        Recipe.SetActive(Recipe.activeSelf ? false : true);
    }
}
