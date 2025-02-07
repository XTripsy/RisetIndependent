using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum eCategory
{
    Pokok,
    Lauk,
    Sayur,
    Buah,
    Sup
}

public class Food : DragDrop
{
    public eCategory current_category;
    [HideInInspector] public string name_menu;
    [HideInInspector] public int maturity_level;
    [SerializeField]
    LayerMask mask;
    Vector3 awake_location;
    Transform slot, temp_slot;
    Collider2D collider2d;
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
        if (bIsSloted && !bIsPressed && temp_slot != null)
        {
            transform.position = temp_slot.position;
        }
    }

    void ClickMouseUp()
    {
        bIsPressed = false;

        collider2d.callbackLayers = 0;

        if (slot != temp_slot && slot != null)
        {
            if(IInventory != null) IInventory.IClose_Inventory();
            temp_slot = slot;
            transform.parent = temp_parent;
            bIsSloted = true;
            return;
        }

        if (temp_parent != transform.parent && temp_parent != null)
        {
            temp_slot = slot;
            if(IInventory != null) IInventory.IClose_Inventory();
            bIsSloted = true;
            transform.parent = temp_parent;
            transform.position = temp_location;
            start_location = temp_location;
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
        if (other.gameObject.tag == "FoodInventory")
        {
            IInventory = other.gameObject.GetComponent<IInterfaceInventory>() as IInterfaceInventory;
        }

        if (other.gameObject.tag == "Droped")
        {
            if (other.name == "Slot_1" && GameManager.CHEF_CONTROLLER.item_holder_1.childCount == 0)
                temp_parent = GameManager.CHEF_CONTROLLER.item_holder_1;
            else if (other.name == "Slot_2" && GameManager.CHEF_CONTROLLER.item_holder_2.childCount == 0)
                temp_parent = GameManager.CHEF_CONTROLLER.item_holder_2;
            else if (other.name == "Slot_3" && GameManager.CHEF_CONTROLLER.item_holder_3.childCount == 0)
                temp_parent = GameManager.CHEF_CONTROLLER.item_holder_3;
            else
                return;

            temp_location = other.transform.position;
            slot = other.transform;
        }

        if (other.gameObject.tag == "Slot" && other.GetComponent<SlotPlating>().current_category == current_category && other.GetComponent<SlotPlating>().holder.childCount == 0)
        {
            temp_parent = other.GetComponent<SlotPlating>().holder;
            temp_location = other.transform.position;
            slot = temp_parent;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "FoodInventory")
        {
            IInventory = null;
        }

        if (other.gameObject.tag == "Droped")
        {
            temp_parent = null;
            start_location = awake_location;
            slot = null;
        }

        if (other.gameObject.tag == "Slot" && other.GetComponent<SlotPlating>().current_category == current_category && other.GetComponent<SlotPlating>().holder.childCount == 0)
        {
            temp_parent = null;
            temp_location = start_location;
            slot = null;
        }
    }
}
