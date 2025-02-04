using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    GameObject ChefChanel, AhliMenuChanel;
    void Awake()
    {
        switch (GameManager.INDEX_PLAYER)
        {
            case 0:
                ChefChanel.gameObject.SetActive(true);
                break;
            case 1:
                AhliMenuChanel.gameObject.SetActive(true);
                break;
        }
    }
}
