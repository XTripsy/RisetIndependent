using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Item : MonoBehaviour
{
    SpriteRenderer sprite_renderer;
    Collider2D collider2d;
    bool bIsMousePressed, bIsDropable;
    Vector3 start_location;

    GameObject GODroped, GOCooked;

    IInterfaceInventory IInventory;

    int index_cooked;
    Tween tween_cooked, tween_display;
    Color item_color;

    void Awake()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
        transform.tag = "Item";
        item_color = sprite_renderer.color;
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
        start_location = transform.position;

        if (tween_cooked != null && tween_cooked.IsPlaying())
        {
            tween_cooked.Kill();
            tween_display.Kill();
        }
    }

    void OnMouseDrag()
    {
        transform.parent = GameManager.CHEF_CONTROLLER.scenes[0].transform;
        transform.position = MouseWorldPosition();
    }

    void OnMouseUp()
    {
        bIsMousePressed = false;
        if (IInventory != null)
        {
            IInventory.IClose_Inventory();
            IInventory = null;
        }

        if (GOCooked != null)
        {
            transform.position = GOCooked.transform.position;
            tween_display = sprite_renderer.DOColor(item_color * 0, 10);
            tween_cooked = DOTween.To(() => index_cooked, x => index_cooked = x, 3, 10).OnUpdate(OnCooked);
            return;
        }

        if (GODroped != null)
        {
            transform.position = GODroped.transform.position;
            return;
        }

        transform.position = start_location;

    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    void OnCooked()
    {
        Color aColor = sprite_renderer.color;
        aColor.a = 255;
        sprite_renderer.color = aColor;
        GOCooked.tag = "Cooked";
        Debug.Log(index_cooked);
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

        if (other.gameObject.tag == "Cook")
            GOCooked = other.gameObject;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Droped")
            GODroped = other.gameObject;

        if (other.gameObject.tag == "Cook")
            GOCooked = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Inventory")
        {
            IInventory = other.gameObject.GetComponent<IInterfaceInventory>() as IInterfaceInventory;

            if (IInventory != null)
                IInventory.IClose_Inventory();
        }

        if (other.gameObject.tag == "Untouchable" || other.gameObject.tag == "Droped")
        {
            GODroped = null;
            other.gameObject.tag = "Droped";
        }

        if (other.gameObject.tag == "Cooked" || other.gameObject.tag == "Cook")
        {
            GOCooked = null;
            other.gameObject.tag = "Cook";
        }
    }
}
