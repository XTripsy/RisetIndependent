using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AhliController : MonoBehaviour
{
    int index_scene;
    public List<GameObject> scenes;
    public List<TextMeshProUGUI> button_text;
    public TextMeshProUGUI title;

    private void Start()
    {
        for (int i = 0; i < scenes.Count; i++)
        {
            Debug.Log("Scene " + i + ": " + scenes[i].name);
        }
    }
    private void Awake()
    {
        GameManager.AHLI_CONTROLLER = this;
        ChangeScene(0);
    }

    public void ChangeScene(int value)
    {
        ChangeVisibilityScene(value);
        ChangeButtonDisplay();
    }

    void ChangeVisibilityScene(int move_value)
    {
        switch (move_value)
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
        button_text[1].text = scenes[2].name;
    }
}
