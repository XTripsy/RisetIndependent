using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public enum ECategoryyy
{
    Pokok,
    Lauk,
    Buah,
    Sayur,
    Soup
}

public class Item : MonoBehaviour
{
    SpriteRenderer sprite_renderer;
    Collider2D collider2d;
    bool bIsMousePressed, bIsDropable, bIsRemove;
    Vector3 start_location, awake_location;

    GameObject GODroped, /*GOCooked,*/ GOSlot;

    IInterfaceInventory IInventory;
    IInterfaceSlotPlating ISlotPlating;

    /*int index_cooked;
    Tween tween_cooked, tween_display;
    Color item_color;*/

    public eCategory eCurrentCategory;

    void Awake()
    {
        awake_location = gameObject.transform.position;
        sprite_renderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
        transform.tag = "Item";
        //item_color = sprite_renderer.color;
    }

    private void Update()
    {
        if (IInventory != null)
            Debug.LogWarning("TAII");

        if (GODroped != null && !bIsMousePressed)
        {
            collider2d.isTrigger = false;
            transform.parent = GameManager.CHEF_CONTROLLER.item_holder.transform;
            transform.position = GODroped.transform.position;
            GODroped.tag = "Untouchable";
        }
    }

    void OnMouseDown()
    {
        collider2d.isTrigger = true;
        bIsMousePressed = true;
        start_location = transform.position;
        GameManager.CHEF_CONTROLLER.trash_bin.transform.DOMoveY(-3.8f, .3f);

        /*if (tween_cooked != null && tween_cooked.IsPlaying())
        {
            tween_cooked.Kill();
            tween_display.Kill();
        }*/
    }

    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition();
    }

    void OnMouseUp()
    {
        collider2d.isTrigger = false;

        GameManager.CHEF_CONTROLLER.trash_bin.transform.DOMoveY(-6.5f, .3f);

        if (bIsRemove)
        {
            Duplicate(1);
            Destroy(transform.gameObject);
        }

        bIsMousePressed = false;
        
        if (IInventory != null)
        {
            Debug.Log("KONTOL");
            IInventory.IClose_Inventory();
            //IInventory = null;
        }

        Duplicate(0);

        if (ISlotPlating != null && GOSlot != null)
        {
            transform.position = (ISlotPlating.IGetCategory() == eCurrentCategory) ? GOSlot.transform.position : start_location;
            transform.parent = GOSlot.transform.parent;
            GOSlot.tag = "Unslot";
            return;
        }    

        /*if (GOCooked != null)
        {
            transform.position = GOCooked.transform.position;
            tween_display = sprite_renderer.DOColor(item_color * 0, 10);
            tween_cooked = DOTween.To(() => index_cooked, x => index_cooked = x, 3, 10).OnUpdate(OnCooked);
            return;
        }*/

        if (GODroped != null)
        {
            transform.position = GODroped.transform.position;
            return;
        }

        transform.position = start_location;
    }

    void Duplicate(int value)
    {
        if (GOSlot == null && GODroped == null)
        {
            GameObject dup = Instantiate(this.gameObject);
            dup.GetComponent<Item>().awake_location = awake_location;
            dup.GetComponent<Item>().eCurrentCategory = eCurrentCategory;
            dup.transform.position = awake_location;
            dup.transform.parent = transform.parent;
        }

        /*GameObject dup;

        switch (value)
        {
            case 0:
                if (start_location == awake_location && (GOSlot != null *//*|| GOCooked != null*//* || GODroped != null))
                {
                    dup = Instantiate(this.gameObject);
                    dup.GetComponent<Item>().awake_location = start_location;
                    dup.GetComponent<Item>().eCurrentCategory = eCurrentCategory;
                    dup.transform.position = start_location;
                    dup.transform.parent = transform.parent;
                }
                break;
            case 1:
                dup = Instantiate(this.gameObject);
                dup.GetComponent<Item>().awake_location = start_location;
                dup.GetComponent<Item>().eCurrentCategory = eCurrentCategory;
                dup.transform.position = start_location;
                dup.transform.parent = transform.parent;
                break;
        }*/
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    /*void OnCooked()
    {
        Color aColor = sprite_renderer.color;
        aColor.a = 255;
        sprite_renderer.color = aColor;
        GOCooked.tag = "Cooked";
        Debug.Log(index_cooked);
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.gameObject.tag == "Inventory" *//*&& index_cooked > 0*//*)
        {
            IInventory = other.gameObject.GetComponent<IInterfaceInventory>() as IInterfaceInventory;

            if (IInventory != null)
                IInventory.IOpen_Inventory();
        }*/
        /*else
        {
            IInventory = other.gameObject.GetComponent<IInterfaceInventory>() as IInterfaceInventory;

            if (IInventory != null)
                IInventory.IClose_Inventory();
        }*/

        if (other.gameObject.tag == "Slot")
        {
            ISlotPlating = other.gameObject.GetComponent<IInterfaceSlotPlating>() as IInterfaceSlotPlating;
            GOSlot = other.gameObject;
        }

        if (other.gameObject.tag == "Droped")
            GODroped = other.gameObject;

        /*if (other.gameObject.tag == "Cook")
            GOCooked = other.gameObject;*/

        if (other.gameObject.tag == "TrashBin")
            bIsRemove = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Slot")
            GOSlot = other.gameObject;

        if (other.gameObject.tag == "Droped")
            GODroped = other.gameObject;

        /*if (other.gameObject.tag == "Cook")
            GOCooked = other.gameObject;*/

        if (other.gameObject.tag == "TrashBin")
            bIsRemove = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Inventory" && IInventory != null)
            IInventory.IClose_Inventory();

        if (other.gameObject.tag == "Slot" || other.gameObject.tag == "Unslot")
        {
            GOSlot = null;
            other.gameObject.tag = "Slot";
        }

        if (other.gameObject.tag == "Untouchable")// || other.gameObject.tag == "Droped")
        {
            GODroped = null;
            other.gameObject.tag = "Droped";
        }

        /*if (other.gameObject.tag == "Cooked" || other.gameObject.tag == "Cook")
        {
            GOCooked = null;
            other.gameObject.tag = "Cook";
        }*/

        if (other.gameObject.tag == "TrashBin")
            bIsRemove = false;
    }
}
