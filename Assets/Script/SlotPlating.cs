using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInterfaceSlotPlating
{
    eCategory IGetCategory();
}

public class SlotPlating : MonoBehaviour, IInterfaceSlotPlating
{
    public eCategory eCurrentCategory;

    public eCategory IGetCategory()
    {
        return eCurrentCategory;
    }
}
