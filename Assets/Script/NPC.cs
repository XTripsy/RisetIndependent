using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    List<Sprite> emotion_sprite;
    [HideInInspector]
    public float emotion;
    [HideInInspector]
    public bool bIsServeed;

    public Dictionary<string, int> menu = new Dictionary<string, int>();
    public Dictionary<string, int> menu_ignore = new Dictionary<string, int>();
    SpriteRenderer emotion_spriteRenderer;
    float time;

    private void Awake()
    {
        emotion_spriteRenderer = GetComponent<SpriteRenderer>();
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

        if (time < 5)
        {
            emotion = 1;
            emotion_spriteRenderer.sprite = emotion_sprite[0];
        }
        else if (time < 10)
        {
            emotion = .5f;
            emotion_spriteRenderer.sprite = emotion_sprite[1];
        }
        else
        {
            emotion = 0;
            emotion_spriteRenderer.sprite = emotion_sprite[2];
        }
    }
}
