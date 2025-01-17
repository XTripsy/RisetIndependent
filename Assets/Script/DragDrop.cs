using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInterfaceDragDrop
{
    void ITriggerEnter(Vector3 location);
    void ITriggerExit();
    void ITriggerParent(GameObject parent);
}

public class DragDrop : MonoBehaviour, IInterfaceDragDrop
{
    public Action MouseDown, MouseDrag, MouseUp;
    protected Vector3 start_location, temp_location;
    protected GameObject temp_parent;

    void OnMouseDown()
    {
        MouseDown();
        GameManager.CHEF_CONTROLLER.trash_bin.transform.DOMoveY(-3.8f, .3f);
    }

    void OnMouseDrag()
    {
        MouseDrag();
        transform.position = MouseWorldPosition();
    }

    void OnMouseUp()
    {
        MouseUp();
        GameManager.CHEF_CONTROLLER.trash_bin.transform.DOMoveY(-6.5f, .3f);
        transform.position = start_location;
    }

    public Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    public void ITriggerEnter(Vector3 location)
    {
        temp_location = location;
    }

    public void ITriggerExit()
    {
        temp_location = start_location;
    }

    public void ITriggerParent(GameObject parent)
    {
        temp_parent = parent;
    }
}
