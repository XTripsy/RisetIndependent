using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunchBox : MonoBehaviour
{
    Dictionary<string, int> Menus = new Dictionary<string, int>();
    int stars = 3;

    public void FinishStage()
    {
        if (Menus.Count != GameManager.GOAL_MENUS.Count)
        {
            stars--;
            stars = Mathf.Clamp(stars, 0, 3);
        }

        foreach (var menu in Menus)
        {
            if (!GameManager.GOAL_MENUS.ContainsKey(menu.Key) || GameManager.GOAL_MENUS[menu.Key] != menu.Value)
            {
                stars--;
                stars = Mathf.Clamp(stars, 0, 3);
                break;
            }

            if (GameManager.IGNORE_MENUS.ContainsKey(menu.Key) || GameManager.IGNORE_MENUS[menu.Key] == menu.Value)
            {
                stars--;
                stars = Mathf.Clamp(stars, 0, 3);
                break;
            }
        }

        Debug.Log(stars);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
            Menus.Add(other.GetComponent<Food>().name_menu, 1);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Food")
            Menus.Remove(other.GetComponent<Food>().name_menu);
    }
}
