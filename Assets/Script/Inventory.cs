using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        rect_transform.DOAnchorPos(close_location, .4f);
    }

    public void IOpen_Inventory()
    {
        rect_transform.DOAnchorPos(open_location, .4f);
    }

    public bool IGetCondition()
    {
        bool result = (rect_transform.position == (Vector3)close_location) ? true : false;
        return result;
    }
}

