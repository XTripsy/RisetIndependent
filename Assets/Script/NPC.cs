using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    List<TextMeshProUGUI> feedback;
    [SerializeField]
    Transform canvas;
    [HideInInspector]
    public float emotion;
    [HideInInspector]
    public bool bIsServeed;

    public Dictionary<string, int> menu = new Dictionary<string, int>();
    public Dictionary<string, int> menu_ignore = new Dictionary<string, int>();

    List<string> ignore_menus = new List<string>();
    string ignore_menu;
    Coroutine feedback_coroutine;
    float time;

    private void Awake()
    {
        menu.Add("AyamRica", 1);
        menu.Add("TelurMalaka", 1);
        //menu_ignore.Add("TelurMalaka", 1);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (bIsServeed)
        {
            gameObject.SetActive(false);
            return;
        }

        time += Time.deltaTime;

        if (time < 20)
        {
            emotion = 1;
            feedback[0].text = "Gembira";
        }
        else if (time < 30)
        {
            emotion = .5f;
            feedback[0].text = "Jenuh";
        }
        else
        {
            emotion = 0;
            feedback[0].text = "Marah";
        }
    }

    /*private IEnumerator DisableFeedback(float delay)
    {
        yield return new WaitForSeconds(delay);
        canvas.gameObject.SetActive(false);
        feedback_coroutine = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Food")
            return;

        if (feedback_coroutine  != null) StopCoroutine(feedback_coroutine);

        canvas.gameObject.SetActive(true);

        feedback[0].text = other.GetComponent<Food>().name_menu == ignore_menu ? "aku gak suka itu" : "iya aku mau";

        switch (other.GetComponent<Food>().maturity_level)
        {
            case 1:
                feedback[1].text = "itu masih mentah tau kak";
                break;
            case 2:
                feedback[1].text = "";
                break;
            case 3:
                feedback[1].text = "ihh itu gosong lho kak, astaga";
                break;
        }

        feedback_coroutine = StartCoroutine(DisableFeedback(2));
    }*/
}
