using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRole : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHostStatusChanged))]
    public bool isHost;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnStartServer()
    {
        isHost = connectionToClient.connectionId == 0;
    }

    public override void OnStartClient()
    {
        if (isLocalPlayer)
        {
            CheckSceneAndSetRole();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (isLocalPlayer && scene.name == "Gameplay")
        {
            CheckSceneAndSetRole();
        }
    }

    private void CheckSceneAndSetRole()
    {
        if (GameplayUIManager.Instance == null)
        {
            Debug.LogError("GameplayUIManager instance is null!");
            return;
        }
        StartCoroutine(SetRoleWhenReady());
    }

    private System.Collections.IEnumerator SetRoleWhenReady()
    {
        while (GameplayUIManager.Instance == null)
        {
            yield return null;
        }
        if (GameplayUIManager.Instance.KokiPanel == null ||
            GameplayUIManager.Instance.GiziPanel == null)
        {
            yield break;
        }

        GameplayUIManager.Instance.SetRole(isHost);
    }

    private void OnHostStatusChanged(bool oldValue, bool newValue)
    {
        if (isLocalPlayer)
        {
            GameplayUIManager.Instance?.SetRole(newValue);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}