using UnityEngine;

public class GameplayUIManager : MonoBehaviour
{
    public static GameplayUIManager Instance;

    [Header("UI References")]
    public GameObject KokiPanel;
    public GameObject GiziPanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        KokiPanel.SetActive(false);
        GiziPanel.SetActive(false);
    }

    public void SetRole(bool isHost)
    {
        KokiPanel.SetActive(isHost);
        GiziPanel.SetActive(!isHost);
    }
}