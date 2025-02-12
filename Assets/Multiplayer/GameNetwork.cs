using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameNetworkManager : NetworkManager
{
    public override void OnStartHost()
    {
        base.OnStartHost();
        GameLobbyManager.Instance.UpdatePlayerCount();
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        Debug.Log($"Connection from {conn.address}");
        base.OnServerConnect(conn);
        GameLobbyManager.Instance.UpdatePlayerCount();
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        if (SceneManager.GetActiveScene().name != "Gameplay")
        {
            StartCoroutine(AddPlayerAfterSceneLoad(conn));
            return;
        }

        base.OnServerAddPlayer(conn);
    }

    private IEnumerator AddPlayerAfterSceneLoad(NetworkConnectionToClient conn)
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Gameplay");
        base.OnServerAddPlayer(conn);
    }

    public override void OnStartServer()
    {
        Debug.Log("[SERVER] Server started - Ready for connections");
        base.OnStartServer();
    }

    public override void OnClientConnect()
    {
        Debug.Log("Successfully connected to " + networkAddress);
        base.OnClientConnect();
    }
}