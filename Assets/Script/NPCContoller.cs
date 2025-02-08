using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCContoller : MonoBehaviour
{
    [SerializeField]
    GameObject npc_1, npc_2, result;
    [HideInInspector]
    public NPC npc_one, npc_two;
    bool bIsDoOnce;

    private void Start()
    {
        GameManager.NPC_CONTROLLER = this;
        npc_one = npc_1.GetComponent<NPC>();
        npc_two = npc_2.GetComponent<NPC>();
    }

    private void Update()
    {
        if ((GameManager.TIME_REMANING <= 250 || npc_one.bIsServeed) && !npc_two.bIsServeed)
            npc_2.SetActive(true);

        if (npc_one.bIsServeed && npc_two.bIsServeed && !bIsDoOnce)
        {
            bIsDoOnce = true;
            if (GameManager.TIME_REMANING > 30) GameManager.TOTAL_STARS += 1;
            Debug.Log(GameManager.TOTAL_STARS);
            result.SetActive(true);
        }
    }

    /*void Update()
    {
        if ((GameManager.TIME_REMANING <= 250 || npc_1 == null) && !bIsDoOnce && npc_2 != null)
        {
            bIsDoOnce = true;
            npc_2.SetActive(true);
        }

        //if (!npc_1.activeSelf && !npc_2.activeSelf)
        if (npc_1 == null && npc_2 == null)
        {
            Debug.LogError(GameManager.TOTAL_STARS);
        }
    }*/
}
