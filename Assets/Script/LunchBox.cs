using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LunchBox : MonoBehaviour
{
    Dictionary<string, int> Menus = new Dictionary<string, int>();
    Dictionary<int, int> Menus_Maturity = new Dictionary<int, int>();
    int stars = 3;
    bool bIsMenuEqual = true;
    bool bIsBurnt;

    public void FinishStage()
    {
        if (GameManager.TIME_REMANING > 30)
            GameManager.TOTAL_STARS += 1;

        foreach (var menu in Menus)
        {
            if (GameManager.GOAL_MENUS[menu.Key] != menu.Value)
                bIsMenuEqual = false;

            if (GameManager.IGNORE_MENUS[menu.Key] == menu.Value)
                bIsBurnt = true;
        }

        if (Menus.Count != GameManager.GOAL_MENUS.Count)
            bIsMenuEqual = false;

        foreach (var item in Menus_Maturity)
        {
            if (item.Value != 2)
                bIsBurnt = true;
        }

        if (bIsMenuEqual)
            GameManager.TOTAL_STARS += 1;

        if (!bIsBurnt)
            GameManager.TOTAL_STARS += 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Food")
            return;

        Menus.Add(other.GetComponent<Food>().name_menu, 1);
        Menus_Maturity.Add(other.GetComponent<Food>().maturity_level, 1);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Food")
            return;

        Menus.Remove(other.GetComponent<Food>().name_menu);
        Menus_Maturity.Remove(other.GetComponent<Food>().maturity_level);
    }
}
