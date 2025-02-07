using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timer_text;
    [SerializeField]
    GameObject game_over;
    float time;

    private void Awake()
    {
        timer_text = GetComponent<TextMeshProUGUI>();
        time = 300;
        GameManager.TIME_REMANING = time;
    }

    void Update()
    {
        int min, sec;

        if (time <= 0)
        {
            GameManager.CHEF_CONTROLLER.gameObject.SetActive(false);
            game_over.SetActive(true);
            time = 0;
            min = Mathf.FloorToInt(time / 60);
            sec = Mathf.FloorToInt(time % 60);
            timer_text.text = string.Format("{0:00}:{1:00}", min, sec);
            GameManager.TIME_REMANING = time;
            return;
        }

        time -= Time.deltaTime;
        min = Mathf.FloorToInt(time / 60);
        sec = Mathf.FloorToInt(time % 60);
        timer_text.text = string.Format("{0:00}:{1:00}", min, sec);
        GameManager.TIME_REMANING = time;
    }
}
