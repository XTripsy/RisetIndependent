using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatingController : MonoBehaviour
{
    public Transform item_holder;
    [SerializeField]
    GameObject lunchbox;

    private void Awake()
    {
        GameManager.PLATING_CONTROLLER = this;
    }

    public void Spawn()
    {
        Instantiate(lunchbox, Vector3.zero, Quaternion.identity, transform.GetChild(0));
    }
}
