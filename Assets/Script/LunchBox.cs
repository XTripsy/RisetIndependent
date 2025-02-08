using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Mirror.BouncyCastle.Asn1.Ocsp;

public class LunchBox : DragDrop
{
    Dictionary<string, int> Menus = new Dictionary<string, int>();
    Dictionary<int, int> Menus_Maturity = new Dictionary<int, int>();
    bool bIsMenuEqual;
    bool bIsBurnt;
    bool bIsServe;
    Tween tween;

    [SerializeField]
    LayerMask mask, layer;
    Vector3 awake_location;
    Transform slot, temp_slot, npc;
    bool bIsSloted, bIsPressed;
    IInterfaceInventory IInventory;
    Collider2D collider2d;

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

        collider2d.callbackLayers = layer;

        if (bIsServe)
        {
            CheckStars();
            //Destroy(npc.gameObject);
            npc.GetComponent<NPC>().bIsServeed = true;
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
        Menus.Clear();
        Menus_Maturity.Clear();

        Dictionary<string, int> menu_siswa = new Dictionary<string, int>();
        Dictionary<string, int> menu_ignore_siswa = new Dictionary<string, int>();

        if (npc.name == "NPC_1")
        {
            menu_siswa = GameManager.NPC_CONTROLLER.npc_one.menu;
            menu_ignore_siswa = GameManager.NPC_CONTROLLER.npc_one.menu_ignore;
        }
        else if (npc.name == "NPC_2")
        {
            menu_siswa = GameManager.NPC_CONTROLLER.npc_two.menu;
            menu_ignore_siswa = GameManager.NPC_CONTROLLER.npc_two.menu_ignore;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponentInChildren<Food>() != null)
            {
                Food food = transform.GetChild(i).GetComponentInChildren<Food>();
                Menus.Add(food.name_menu, 1);
                Menus_Maturity.Add(i+1, food.maturity_level);
            }
        }

        if (Menus.Count != menu_siswa.Count)
        {
            bIsMenuEqual = false;
            return;
        }

        foreach (var item in Menus_Maturity)
        {
            if (item.Value == 2)
                bIsBurnt = true;
        }

        foreach (var menu in Menus)
        {
            if (menu_siswa[menu.Key] == menu.Value)
                bIsMenuEqual = true;

            foreach (var item in menu_ignore_siswa)
            {
                if (item.Value == menu.Value)
                {
                    bIsMenuEqual = false;
                    return;
                }
            }
        }

        Debug.Log(bIsMenuEqual +"/"+ bIsBurnt);

        if (!bIsMenuEqual || !bIsBurnt)
            return;

        if (npc.name == "NPC_1")
            GameManager.TOTAL_STARS += GameManager.NPC_CONTROLLER.npc_one.emotion;

        if (npc.name == "NPC_2")
            GameManager.TOTAL_STARS += GameManager.NPC_CONTROLLER.npc_two.emotion;

        Debug.Log("NPC" + GameManager.TOTAL_STARS);
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
        /*if (other.tag == "Food")
        {
            Menus.Add(other.GetComponent<Food>().name_menu, 1);
            Menus_Maturity.Add(other.GetComponent<Food>().maturity_level, 1);
            Debug.Log("MASUK FOOD");
        }*/

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
        /*if (other.tag == "Food")
        {
            Menus.Remove(other.GetComponent<Food>().name_menu);
            Menus_Maturity.Remove(other.GetComponent<Food>().maturity_level);
            Debug.Log("KELUARFOOD");
        }*/

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
