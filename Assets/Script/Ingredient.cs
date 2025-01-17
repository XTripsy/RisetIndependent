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
    void ISetParent(GameObject parent);
}

public class Ingredient : DragDrop, IInterfaceIngredient
{
    public eIngredient current_ingredient_type;

    GameObject parent;

    private void Awake()
    {
        start_location = transform.position;
        temp_location = start_location;
        temp_parent = transform.parent.gameObject;
        MouseUp = new Action(ClickMouseUp);
        MouseDrag = new Action(ClickMouseHold);
        MouseDown = new Action(ClickMouseDown);
    }

    void ClickMouseUp()
    {
        start_location = temp_location;
        if(temp_parent != null) transform.parent = temp_parent.transform;
    }

    void ClickMouseHold()
    {

    }

    void ClickMouseDown() 
    {

    }

    public void ISetParent(GameObject parent)
    {
        throw new NotImplementedException();
    }
}
