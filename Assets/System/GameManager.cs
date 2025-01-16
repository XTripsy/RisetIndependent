using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
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
}
