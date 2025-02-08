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
    public static PlatingController PLATING_CONTROLLER;
    public static NPCContoller NPC_CONTROLLER;
    public static Dictionary<string, int> GOAL_MENUS = new Dictionary<string, int>();
    public static Dictionary<string, int> IGNORE_MENUS = new Dictionary<string, int>();
}
