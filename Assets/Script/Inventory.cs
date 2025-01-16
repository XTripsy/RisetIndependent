using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInterfaceInventory
{
    void IOpen_Inventory();
    void IClose_Inventory();
    bool IGetCondition();
}

public class Inventory : MonoBehaviour, IInterfaceInventory
{
    public Vector2 open_location;
    Vector2 close_location;
    RectTransform rect_transform;

    private void Awake()
    {
        rect_transform = GetComponent<RectTransform>();
        close_location = rect_transform.position;
    }

    private void OnMouseDown()
    {
        if (IGetCondition())
            IOpen_Inventory();
        else
            IClose_Inventory();
    }

    public void IClose_Inventory()
    {
        rect_transform.position = close_location;
    }

    public void IOpen_Inventory()
    {
        rect_transform.position = open_location;
    }

    public bool IGetCondition()
    {
        bool result = (rect_transform.position == (Vector3)close_location) ? true : false;
        return result;
    }
}

