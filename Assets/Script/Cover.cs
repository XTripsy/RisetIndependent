using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Cover : MonoBehaviour
{
    public List<GameObject> book;
    public TMP_InputField inputfield;
    public List<GameObject> fpage;

    public void CheckInput()
    {
        if (inputfield.text == "342") 
        {
            Debug.Log("Password accepted");
            foreach (var item in book)
            {
                item.SetActive(true);
            }
            foreach (var item in fpage)
            {
                item.SetActive(false);
            }
        }
    }
}
