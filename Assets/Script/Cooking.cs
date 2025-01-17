using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class Cooking : MonoBehaviour
{
    bool ayam, telur, wortel, cabai, bawang_putih, bawang_merah;

    int index_fire;
    bool bIsHover;
    Collider2D collider2d;
    SpriteRenderer sprite_renderer;

    [SerializeField]
    eCookingType current_cooking_type;
    eFireType current_fire_type;

    [SerializeField]
    LayerMask layer_mask;
    [SerializeField]
    GameObject holder;

    private void Awake()
    {
        collider2d = GetComponent<Collider2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        collider2d.callbackLayers = index_fire != 0 ? layer_mask : 0;

        switch (current_cooking_type)
        {
            case eCookingType.Fry:
                RicaRicaCheck();
                break;
            case eCookingType.Grill:
                break;
            case eCookingType.Boil:
                break;
        }

        IngredientsCheck();

        if (!bIsHover) return;

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

    private void FireType(Action kecil, Action sedang, Action besar)
    {
        switch (current_fire_type)
        {
            case eFireType.Kecil:
                kecil.Invoke();
                break;
            case eFireType.Sedang:
                sedang.Invoke();
                break;
            case eFireType.Besar:
                besar.Invoke();
                break;
        }
    }

    void RicaRicaCheck()
    {
        if (ayam && cabai && bawang_merah && bawang_putih)
        {
            switch (current_fire_type)
            {
                case eFireType.Kecil:
                    Debug.LogWarning("RicaRica Belum Matang");
                    break;
                case eFireType.Sedang:
                    Debug.LogWarning("RicaRica Matang");
                    break;
                case eFireType.Besar:
                    Debug.LogWarning("RicaRica Gosong");
                    break;
            }

            for (int i = 0; i < holder.transform.childCount; i++)
            {
                Destroy(holder.transform.GetChild(i).gameObject);
            }
        }
    }

    void IngredientsCheck()
    {
        for (int i = 0; i < holder.transform.childCount; i++)
        {
            switch (holder.transform.GetChild(i).GetComponent<Ingredient>().current_ingredient_type)
            {
                case eIngredient.Ayam:
                    ayam = true;
                    break;
                case eIngredient.Telur:
                    telur = true;
                    break;
                case eIngredient.Wortel:
                    wortel = true;
                    break;
                case eIngredient.Cabai:
                    cabai = true;
                    break;
                case eIngredient.BawangPutih:
                    bawang_putih = true;
                    break;
                case eIngredient.BawangMerah:
                    bawang_merah = true;
                    break;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (transform.childCount == 0)
            bIsHover = true;
    }

    private void OnMouseExit()
    {
        if (transform.childCount == 0)
            bIsHover = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ingredient")
        {
            IInterfaceDragDrop IDragDrop = other.gameObject.GetComponent<IInterfaceDragDrop>() as IInterfaceDragDrop;
            IDragDrop.ITriggerEnter(this.transform.position);
            IDragDrop.ITriggerParent(holder);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "DragDrop")
        {
            IInterfaceDragDrop IDragDrop = other.gameObject.GetComponent<IInterfaceDragDrop>() as IInterfaceDragDrop;
            IDragDrop.ITriggerExit();
            IDragDrop.ITriggerParent(null);
        }
    }
}
