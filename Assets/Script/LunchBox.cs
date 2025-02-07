using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LunchBox : DragDrop
{
    Dictionary<string, int> Menus = new Dictionary<string, int>();
    Dictionary<int, int> Menus_Maturity = new Dictionary<int, int>();
    //int stars = 3;
    bool bIsMenuEqual = true;
    bool bIsBurnt;
    bool bIsServe;
    Tween tween;

    [SerializeField]
    LayerMask mask;
    Vector3 awake_location;
    Transform slot, temp_slot, npc;
    bool bIsSloted, bIsPressed;
    IInterfaceInventory IInventory;
    Collider2D collider2d;

    #region FINISH_BUTTON

    public void FinishStage()
    {
        if (GameManager.TIME_REMANING > 30)
            GameManager.TOTAL_STARS += 1;

        foreach (var menu in Menus)
        {
            if (GameManager.GOAL_MENUS[menu.Key] != menu.Value)
                bIsMenuEqual = false;

            if (GameManager.IGNORE_MENUS[menu.Key] == menu.Value)
                bIsBurnt = true;
        }

        if (Menus.Count != GameManager.GOAL_MENUS.Count)
            bIsMenuEqual = false;

        foreach (var item in Menus_Maturity)
        {
            if (item.Value != 2)
                bIsBurnt = true;
        }

        if (bIsMenuEqual)
            GameManager.TOTAL_STARS += 1;

        if (!bIsBurnt)
            GameManager.TOTAL_STARS += 1;
    }

    #endregion

    private void Start()
    {
        start_location = transform.position;
        awake_location = start_location;
        start_location = transform.position;
        temp_location = start_location;
        temp_parent = transform.parent;
        MouseUp = new Action(ClickMouseUp);
        MouseDrag = new Action(ClickMouseHold);
        MouseDown = new Action(ClickMouseDown);
        collider2d = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (bIsSloted && !bIsPressed && temp_slot != null && !bIsServe)
        {
            transform.position = temp_slot.position;
        }
    }

    void ClickMouseUp()
    {
        bIsPressed = false;

        collider2d.callbackLayers = 0;

        if (bIsServe)
        {
            CheckStars();
            //npc.gameObject.SetActive(false);
            Destroy(npc.gameObject);
            Destroy(gameObject);
            return;
        }

        if (slot != temp_slot && slot != null)
        {
            if (IInventory != null) IInventory.IClose_Inventory();
            temp_slot = slot;
            transform.parent = temp_parent;
            bIsSloted = true;
            GameManager.PLATING_CONTROLLER.Spawn();
            return;
        }

        if (temp_parent != transform.parent && temp_parent != null)
        {
            temp_slot = slot;
            if (IInventory != null) IInventory.IClose_Inventory();
            bIsSloted = true;
            transform.parent = temp_parent;
            transform.position = temp_location;
            start_location = temp_location;
        }
    }

    void CheckStars()
    {
        foreach (var menu in Menus)
        {
            if (GameManager.GOAL_MENUS[menu.Key] != menu.Value)
                bIsMenuEqual = false;

            if (GameManager.IGNORE_MENUS[menu.Key] == menu.Value)
                bIsMenuEqual = false;
        }

        if (Menus.Count != GameManager.GOAL_MENUS.Count)
            bIsMenuEqual = false;

        foreach (var item in Menus_Maturity)
        {
            if (item.Value != 2)
                bIsMenuEqual = true;
        }

        if (!bIsMenuEqual)
            return;

        if (npc.name == "NPC_1")
            GameManager.TOTAL_STARS += GameManager.NPC_CONTROLLER.npc_one.emotion;

        if (npc.name == "NPC_2")
            GameManager.TOTAL_STARS += GameManager.NPC_CONTROLLER.npc_two.emotion;
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
        if (other.tag == "Food")
        {
            Menus.Add(other.GetComponent<Food>().name_menu, 1);
            Menus_Maturity.Add(other.GetComponent<Food>().maturity_level, 1);
        }

        if (other.gameObject.tag == "MenuInventory")
        {
            IInventory = other.gameObject.GetComponent<IInterfaceInventory>() as IInterfaceInventory;
        }

        if (other.gameObject.tag == "Droped")
        {
            if (GameManager.PLATING_CONTROLLER.item_holder.childCount > 0)
                return;

            temp_parent = GameManager.PLATING_CONTROLLER.item_holder;
            temp_location = other.transform.position;
            slot = other.transform;
            tween = transform.DOScale(.35f, .2f);
        }

        if (other.tag == "NPC")
        {
            npc = other.transform;
            bIsServe = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Menus.Remove(other.GetComponent<Food>().name_menu);
            Menus_Maturity.Remove(other.GetComponent<Food>().maturity_level);
        }

        if (other.gameObject.tag == "MenuInventory")
        {
            IInventory = null;
        }

        if (other.gameObject.tag == "Droped")
        {
            if (transform.parent != GameManager.PLATING_CONTROLLER.item_holder)
                tween = transform.DOScale(1.0f, .2f);

            temp_parent = null;
            start_location = awake_location;
            slot = null;
        }

        if (other.tag == "NPC")
        {
            npc = null;
            bIsServe = false;
        }
    }
}
