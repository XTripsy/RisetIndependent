using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : MonoBehaviour
{
    int index_fire;
    bool bIsHover;
    SpriteRenderer sprite_renderer;

    private void Awake()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!bIsHover)
            return;

        index_fire += (int)Input.GetAxis("Mouse ScrollWheel");
        index_fire %= 4;

        switch (index_fire)
        {
            case -3:
                sprite_renderer.color = Color.yellow;
                Debug.Log("Kecil");
                break;
            case -2:
                sprite_renderer.color = Color.red;
                Debug.Log("Sedang");
                break;
            case -1:
                sprite_renderer.color = Color.blue;
                Debug.Log("Besar");
                break;
            case 0:
                sprite_renderer.color = Color.black;
                Debug.Log("Mati");
                break;
            case 1:
                sprite_renderer.color = Color.yellow;
                Debug.Log("Kecil");
                break;
            case 2:
                sprite_renderer.color = Color.red;
                Debug.Log("Sedang");
                break;
            case 3:
                sprite_renderer.color = Color.blue;
                Debug.Log("Besar");
                break;
        }
    }

    private void OnMouseEnter()
    {
        bIsHover = true;
    }

    private void OnMouseExit()
    {
        bIsHover = false;
    }
}
