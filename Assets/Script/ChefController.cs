using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChefController : MonoBehaviour
{
    int index_scene;
    public Transform item_holder_1, item_holder_2, item_holder_3, trash_bin;
    public List<GameObject> scenes;
    public List<TextMeshProUGUI> button_text;
    public TextMeshProUGUI title;

    IInterfaceInventory IInventory;

    private void Awake()
    {
        GameManager.CHEF_CONTROLLER = this;
        ChangeScene(0);
    }

    public void ChangeScene(int value)
    {
        ChangeVisibilityScene(value);
        ChangeButtonDisplay();
    }

    void ChangeVisibilityScene(int move_value)
    {
        switch(move_value)
        {
            case -1:
                scenes.Move(0, scenes.Count - 1);
                break;
            case 1:
                scenes.Move(scenes.Count - 1, 0);
                break;
        }

        for (int i = 0; i < scenes.Count; i++)
        {
            if (i > 0)
                scenes[i].SetActive(false);
            else
                scenes[i].SetActive(true);
        }
    }

    void ChangeButtonDisplay()
    {
        title.text = scenes[0].name;
        button_text[0].text = scenes[1].name;
        button_text[1].text = scenes[1].name;
    }
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

