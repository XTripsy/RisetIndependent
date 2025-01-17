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

    [SerializeField]
    GameObject[] slots;
    Vector2 close_location;
    RectTransform rect_transform;
    Tween tween;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Food")
            IOpen_Inventory();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Food")
            IClose_Inventory();
    }

    public void IClose_Inventory()
    {
        tween.Kill();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Collider2D>().enabled = false;    
        }

        tween = rect_transform.DOAnchorPos(close_location, .4f).OnComplete(CompleteTween);
    }

    public void IOpen_Inventory()
    {
        tween.Kill();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Collider2D>().enabled = false;
        }

        tween = rect_transform.DOAnchorPos(open_location, .4f).OnComplete(CompleteTween);
    }

    void CompleteTween()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Collider2D>().enabled = true;
        }
    }

    public bool IGetCondition()
    {
        bool result = (rect_transform.position == (Vector3)close_location) ? true : false;
        return result;
    }
}

