using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookmark : MonoBehaviour
{
    public List<GameObject> Page;
    public GameObject Openpage;

    void OnMouseDown()
    {
        foreach (var item in Page)
        {
            item.SetActive(false);
        }
        Openpage.SetActive(true);
    }
}
