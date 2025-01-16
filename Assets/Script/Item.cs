using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    Collider2D collider2d;

    GameObject GODroped;

    IInterfaceInventory IInventory;
    bool bIsMousePressed;

    void Awake()
    {
        collider2d = GetComponent<Collider2D>();
        transform.tag = "Item";
    }

    private void Update()
    {
        if (GODroped != null && !bIsMousePressed)
        {
            transform.parent = GameManager.CHEF_CONTROLLER.item_holder.transform;
            transform.position = GODroped.transform.position;
            GODroped.tag = "Untouchable";
        }
    }

    void OnMouseDown()
    {
        bIsMousePressed = true;
    }

    void OnMouseDrag()
    {
        transform.parent = GameManager.CHEF_CONTROLLER.scenes[0].transform;
        transform.position = MouseWorldPosition();
    }

    void OnMouseUp()
    {
        bIsMousePressed = false;

        if (GODroped != null)
        {
            transform.position = GODroped.transform.position;
        }

        if (IInventory != null)
        {
            IInventory.IClose_Inventory();
            IInventory = null;
        }
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Inventory")
        {
            IInventory = other.gameObject.GetComponent<IInterfaceInventory>() as IInterfaceInventory;

            if (IInventory != null)
                IInventory.IOpen_Inventory();
        }

        if (other.gameObject.tag == "Droped")
            GODroped = other.gameObject;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Droped")
            GODroped = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Inventory")
        {
            IInventory = other.gameObject.GetComponent<IInterfaceInventory>() as IInterfaceInventory;

            if (IInventory != null)
                IInventory.IClose_Inventory();
        }

        if (other.gameObject.tag == "Droped")
            GODroped = null;

        if (other.gameObject.tag == "Untouchable")
        {
            GODroped = null;
            other.gameObject.tag = "Droped";
        }
    }
}
