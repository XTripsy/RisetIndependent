using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class GameplayUIManager : MonoBehaviour
{
    public static GameplayUIManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject KokiPanel;
    public GameObject GiziPanel;

    [SerializeField] private Canvas mainCanvas;  // Reference to the main canvas

    void Awake()
    {
        // Ensure this component persists across scene loads
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Ensure UI components are properly referenced
        if (KokiPanel == null || GiziPanel == null)
        {
            Debug.LogError("UI Panels not assigned in GameplayUIManager!");
            return;
        }

        KokiPanel.SetActive(false);
        GiziPanel.SetActive(false);

        if (mainCanvas != null)
        {
            mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            mainCanvas.sortingOrder = 1;
        }
    }

    public void SetRole(bool isHost)
    {
        if (!Utilities.IsMainThread())
        {
            MainThreadDispatcher.Execute(() => SetRoleInternal(isHost));
            return;
        }
        SetRoleInternal(isHost);
    }

    private void SetRoleInternal(bool isHost)
    {
        if (KokiPanel == null || GiziPanel == null)
        {
            Debug.LogError("Panels are null in GameplayUIManager!");
            return;
        }

        Debug.Log($"Setting role - IsHost: {isHost}");
        KokiPanel.SetActive(isHost);
        GiziPanel.SetActive(!isHost);
    }

    private static class MainThreadDispatcher
    {
        private static readonly System.Collections.Generic.Queue<System.Action> executionQueue =
            new System.Collections.Generic.Queue<System.Action>();

        public static void Execute(System.Action action)
        {
            if (action == null) return;
            executionQueue.Enqueue(action);
        }

        public static void Update()
        {
            while (executionQueue.Count > 0)
            {
                executionQueue.Dequeue().Invoke();
            }
        }
    }

    private static class Utilities
    {
        public static bool IsMainThread()
        {
            return System.Threading.Thread.CurrentThread.ManagedThreadId == 1;
        }
    }

    void Update()
    {
        MainThreadDispatcher.Update();
    }
}