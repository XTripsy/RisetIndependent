using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    public TMP_InputField inputfield;
    public GameObject Data;

    public void CheckPass()
    {
        if (inputfield.text == "342")
        {
            Data.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
