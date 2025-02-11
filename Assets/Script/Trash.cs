using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        IInterfaceDragDrop IDragDrop = other.gameObject.GetComponent<IInterfaceDragDrop>() as IInterfaceDragDrop;
        IDragDrop.IDestroy(true);

        if (other.gameObject.tag == "Ingredient")
        {
            IInterfaceIngredient IIngredient = other.gameObject.GetComponent<IInterfaceIngredient>() as IInterfaceIngredient;
            IIngredient.ISpawn(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInterfaceDragDrop IDragDrop = other.gameObject.GetComponent<IInterfaceDragDrop>() as IInterfaceDragDrop;
        IDragDrop.IDestroy(false);

        if (other.gameObject.tag == "Ingredient")
        {
            IInterfaceIngredient IIngredient = other.gameObject.GetComponent<IInterfaceIngredient>() as IInterfaceIngredient;
            IIngredient.ISpawn(false);
        }
    }
}
