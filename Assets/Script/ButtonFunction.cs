using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunction : MonoBehaviour
{
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void SetPlayerIndex(int index)
    {
        GameManager.INDEX_PLAYER = index;
    }
}
