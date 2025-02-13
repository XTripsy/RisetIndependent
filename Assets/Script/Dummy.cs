using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("test");
        gameObject.SetActive(false);
    }
}
