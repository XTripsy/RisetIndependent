using System;
using UnityEngine;

public class GameEventSystem : MonoBehaviour
{
    public static GameEventSystem Instance { get; private set; }
    public Action<string> OnCodeGenerated;
    public Action OnBookUnlocked;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerCodeGenerated(string code)
    {
        OnCodeGenerated?.Invoke(code);
    }

    public void TriggerBookUnlocked()
    {
        OnBookUnlocked?.Invoke();
    }
}
