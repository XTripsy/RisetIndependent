using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInterfaceSlotPlating
{
    eCategory IGetCategory();
}

public class SlotPlating : MonoBehaviour, IInterfaceSlotPlating
{
    public eCategory current_category;
    public Transform holder;

    public eCategory IGetCategory()
    {
        return current_category;
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (holder.childCount > 0) return;

        if (other.gameObject.tag == "Food" && other.GetComponent<Food>().current_category == current_category)
        {
            IInterfaceDragDrop IDragDrop = other.gameObject.GetComponent<IInterfaceDragDrop>() as IInterfaceDragDrop;
            IDragDrop.ITriggerEnter(holder.position);
            IDragDrop.ITriggerParent(holder);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Food")
        {
            IInterfaceDragDrop IDragDrop = other.gameObject.GetComponent<IInterfaceDragDrop>() as IInterfaceDragDrop;
            IDragDrop.ITriggerExit();
            IDragDrop.ITriggerParent(null);
        }
    }*/
}
