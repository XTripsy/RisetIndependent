using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject Recipe;
    public bool BookOpen;
    void OnMouseDown()
    {
        Interact();
    }
    public void Interact()
    {
        if (!BookOpen)
        {
            Recipe.SetActive(true);
            BookOpen = true;
        } else
        {
            Recipe.SetActive(false);
            BookOpen = false;
        }
        
    }
}
