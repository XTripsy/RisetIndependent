using Mirror;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net;
using System.Linq;


public class GameLobbyManager : MonoBehaviour
{
    public static GameLobbyManager Instance;

    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject PlayerSelectionPanel;
    public GameObject hostWaitPanel;
    public GameObject clientConnectPanel;
    public GameObject errorPanel;
    public GameObject startGamePanel;


    [Header("References")]
    public TMP_Text errorText;
    public TMP_InputField ipInputField;
    public TMP_Text connectingStatusText;
    public TMP_Text hostIPText;

    private bool isHost;
    public NetSceneController sceneController;
    public GameNetworkManager networkManagerPrefab;
    private GameNetworkManager networkManager;
    void Awake()
    {
        networkManager = Instantiate(networkManagerPrefab);
        DontDestroyOnLoad(networkManager.gameObject);
        Instance = this;
        ShowPanel(mainMenuPanel);
    }

    public void OnPlayClicked()
    {
        ShowPanel(PlayerSelectionPanel);
    }

    public void OnHostClicked()
    {
        isHost = true;
        networkManager.StartHost();
        ShowPanel(hostWaitPanel);
        hostIPText.text = "Your IP: " + GetLocalIP();
    }

    public string GetLocalIP()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return "127.0.0.1";
    }

    private void CheckHostConnections()
    {
        Debug.Log($"Current players: {networkManager.numPlayers}");
        UpdatePlayerCount();
    }

    public void OnClientClicked()
    {
        ShowPanel(clientConnectPanel);
        connectingStatusText.gameObject.SetActive(false);
    }

    public void OnClientConnectClicked()
    {
        if (!string.IsNullOrEmpty(ipInputField.text))
        {
            networkManager.networkAddress = ipInputField.text;
            networkManager.StartClient();
            StartCoroutine(ConnectionTimeoutCheck());
        }

    }

    private System.Collections.IEnumerator ConnectionTimeoutCheck()
    {
        yield return new WaitForSeconds(5f);
        if (!NetworkClient.isConnected)
        {
            ShowError("Connection timed out!");
            connectingStatusText.gameObject.SetActive(false);
        }
    }

    public void UpdatePlayerCount()
    {
        Debug.Log($"Connections: {NetworkServer.connections.Count}");

        if (isHost && NetworkServer.connections.Count >= 2)
        {
            ShowPanel(startGamePanel);
        }
    }

    public void ShowError(string message)
    {
        errorText.text = message;
        ShowPanel(errorPanel);
    }

    public void ShowPanel(GameObject panel)
    {
        if (panel == null || !panel.scene.IsValid()) return;

        mainMenuPanel.SetActive(false);
        PlayerSelectionPanel.SetActive(false);
        hostWaitPanel.SetActive(false);
        clientConnectPanel.SetActive(false);
        errorPanel.SetActive(false);
        startGamePanel.SetActive(false);

        if (panel != null) panel.SetActive(true);
    }

    public void ReturnToMenu()
    {
        if (NetworkServer.active)
        {
            NetworkManager.singleton.StopHost();
        }
        else if (NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopClient();
        }
        ShowPanel(mainMenuPanel);
    }
    public void StartGame()
    {
        if (isHost)
        {
            sceneController.MoveAllToGameplayScene();
        }
    }

    void Start()
    {
        NetworkClient.OnConnectedEvent += OnClientConnected;
        NetworkClient.OnDisconnectedEvent += OnClientDisconnected;
    }

    void OnDestroy()
    {
        NetworkClient.OnConnectedEvent -= OnClientConnected;
        NetworkClient.OnDisconnectedEvent -= OnClientDisconnected;
    }



    private void OnClientConnected()
    {
        Debug.Log("Client connected successfully!");
        connectingStatusText.text = "Connected!";

        if (!isHost)
        {
            NetworkClient.Send(new ReadyMessage());
        }
    }

    private void OnClientDisconnected()
    {
        Debug.LogWarning("Connection lost!");
        ShowError("Connection failed!");
        connectingStatusText.gameObject.SetActive(false);
    }


}