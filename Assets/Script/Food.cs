using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Food : DragDrop
{
    Vector3 awake_location;
    Transform slot, temp_slot;
    Collider2D collider2d;
    [SerializeField]
    LayerMask mask;
    bool bIsSloted, bIsPressed;
    IInterfaceInventory IInventory;

    private void Start()
    {
        start_location = transform.position;
        awake_location = start_location;
        temp_location = start_location;
        temp_parent = transform.parent;
        MouseUp = new Action(ClickMouseUp);
        MouseDrag = new Action(ClickMouseHold);
        MouseDown = new Action(ClickMouseDown);
        collider2d = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (bIsSloted && !bIsPressed)
        {
            transform.position = temp_slot.position;
        }
    }

    void ClickMouseUp()
    {
        bIsPressed = false;

        collider2d.callbackLayers = 0;

        if (slot != temp_slot && temp_slot != null)
        {
            temp_slot = slot;
            bIsSloted = true;
            return;
        }

        if (temp_parent != transform.parent && temp_parent != null)
        {
            temp_slot = slot;
            if(IInventory != null) IInventory.IClose_Inventory();
            bIsSloted = true;
            transform.parent = temp_parent;
            transform.position = start_location;
        }
    }

    void ClickMouseHold()
    {

    }

    void ClickMouseDown()
    {
        bIsPressed = true;
        collider2d.callbackLayers = mask;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Inventory")
        {
            IInventory = other.gameObject.GetComponent<IInterfaceInventory>() as IInterfaceInventory;
        }

        if (other.gameObject.tag == "Droped")
        {
            temp_parent = GameManager.CHEF_CONTROLLER.item_holder;
            start_location = other.transform.position;
            slot = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Inventory")
        {
            IInventory = null;
        }

        if (other.gameObject.tag == "Droped")
        {
            temp_parent = null;
            start_location = awake_location;
            slot = null;
        }
    }
}
