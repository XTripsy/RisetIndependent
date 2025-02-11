using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetSceneController : NetworkBehaviour
{
    [Scene] public string gameplayScene;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void MoveAllToGameplayScene()
    {
        if (isServer)
        {
            NetworkManager.singleton.ServerChangeScene(gameplayScene);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == gameplayScene)
        {
            if (NetworkClient.localPlayer != null)
            {
                PlayerRole localPlayer = NetworkClient.localPlayer.GetComponent<PlayerRole>();
                GameplayUIManager.Instance.SetRole(localPlayer.isHost);
            }
        }
    }
}
