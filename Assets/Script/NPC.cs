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

    List<string> ignore_menus = new List<string>();
    string ignore_menu;
    Coroutine feedback_coroutine;

    private void Awake()
    {
        /*ignore_menus.Add("AyamRica");
        ignore_menus.Add("Semangka");
        ignore_menus.Add("AyamBali");
        ignore_menus.Add("Nasi");
        ignore_menus.Add("Kentang");
        ignore_menus.Add("kol");

        ignore_menu = ignore_menus[Random.Range(0, ignore_menus.Count-1)];*/
        GameManager.IGNORE_MENUS.Add("Nasi", 1);
    }

    private void Update()
    {
        if (GameManager.TIME_REMANING < 30)
            feedback[2].text = "MARAH";
        else if (GameManager.TIME_REMANING < 180)
            feedback[2].text = "JENUH";
        else 
            feedback[2].text = "GEMBIRA";
    }

    private IEnumerator DisableFeedback(float delay)
    {
        yield return new WaitForSeconds(delay);
        canvas.gameObject.SetActive(false);
        feedback_coroutine = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogError("KOTOL");

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
    }
}
