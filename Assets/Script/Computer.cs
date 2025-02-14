using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public GameObject Screen;
    public bool ComptOpen;
    void OnMouseDown()
    {
        Interact();
    }
    public void Interact()
    {
        if (!ComptOpen)
        {
            Screen.SetActive(true);
            ComptOpen = true;
        }
        else
        {
            Screen.SetActive(false);
            ComptOpen = false;
        }

    }
}
