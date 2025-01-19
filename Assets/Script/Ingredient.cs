using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eIngredient
{
    Ayam,
    Telur,
    Wortel,
    Cabai,
    BawangPutih,
    BawangMerah
}

interface IInterfaceIngredient
{
    void ISpawn(bool value);
}

public class Ingredient : DragDrop, IInterfaceIngredient
{
    public eIngredient current_ingredient_type;
    bool bIsSpawn, bIsDoOnce;
    public Transform awake_parent, current_parent;

    private void Start()
    {
        awake_parent = transform.parent;
        current_parent = awake_parent;
        start_location = transform.position;
        temp_location = start_location;
        temp_parent = transform.parent;
        MouseUp = new Action(ClickMouseUp);
        MouseDrag = new Action(ClickMouseHold);
        MouseDown = new Action(ClickMouseDown);
    }

    void ClickMouseUp()
    {
        if (bIsSpawn && transform.parent == awake_parent)
        {
            GameObject dup = Instantiate(this.gameObject);
            /*dup.GetComponent<Ingredient>().start_location = start_location;
            dup.GetComponent<Ingredient>().awake_parent = awake_parent;
            dup.GetComponent<Ingredient>().current_parent = awake_parent;*/
            dup.GetComponent<Ingredient>().current_ingredient_type = current_ingredient_type;
            dup.transform.position = start_location;
            dup.transform.parent = transform.parent;
        }

        start_location = temp_location;

        if (temp_parent != awake_parent && !bIsDoOnce)
        {
            bIsDoOnce = true;
            transform.parent = temp_parent.transform;
            current_parent = temp_parent;
            transform.localScale = new Vector3(.2f, .2f, .2f);
        }

        if (temp_parent != awake_parent || bIsDoOnce) transform.localScale = new Vector3(.2f, .2f, .2f);
    }

    void ClickMouseHold()
    {

    }

    void ClickMouseDown() 
    {
        transform.localScale = Vector3.one;
    }

    public void ISpawn(bool value)
    {
        bIsSpawn = value;
    }
}
