using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public static int INDEX_PLAYER;
    public static float TIME_REMANING;
    public static float TOTAL_STARS;
    private static GameManager game_manager;
    public static GameManager GAME_MANAGER
    {
        get
        {
            if (game_manager == null)
                game_manager = new GameManager();

            return game_manager;
        }
    }

    public static ChefController CHEF_CONTROLLER;
    public static AhliController AHLI_CONTROLLER;
    public static PlatingController PLATING_CONTROLLER;
    public static NPCContoller NPC_CONTROLLER;
    public static Dictionary<string, int> GOAL_MENUS = new Dictionary<string, int>();
    public static Dictionary<string, int> IGNORE_MENUS = new Dictionary<string, int>();
}

public static class ListExtensions
{
    public static void Move<T>(this List<T> list, int oldIndex, int newIndex)
    {
        if (oldIndex < 0 || oldIndex >= list.Count || newIndex < 0 || newIndex >= list.Count)
            return;

        T item = list[oldIndex];
        list.RemoveAt(oldIndex);
        list.Insert(newIndex, item);
    }
}
