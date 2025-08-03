using UnityEngine;
using Unity.Netcode;

namespace ShutTheSquare.Core
{
    public class GameManager : NetworkBehaviour
    {
        [Header("Game State")]
        [SerializeField] private GameState currentGameState = GameState.WaitingForPlayers;
        
        [Header("Mobile Optimization")]
        [SerializeField] private bool enableMobileOptimizations = true;
        
        public static GameManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeMobileSettings();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void InitializeMobileSettings()
        {
            if (enableMobileOptimizations)
            {
                // Set target frame rate for mobile
                Application.targetFrameRate = GameConstants.TARGET_FRAMERATE;
                
                // Disable screen dimming during gameplay
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
                
                // Configure for mobile performance
                QualitySettings.vSyncCount = 0;
                
#if UNITY_ANDROID || UNITY_IOS
                // Mobile-specific optimizations
                QualitySettings.antiAliasing = 2; // 2x MSAA for mobile
                
                // Configure accelerometer for shake detection
                Input.gyro.enabled = true;
#endif
            }
        }
        
        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
                NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
            }
        }
        
        private void OnClientConnected(ulong clientId)
        {
            Debug.Log($"Client {clientId} connected to game");
            // Handle player joining logic
        }
        
        private void OnClientDisconnected(ulong clientId)
        {
            Debug.Log($"Client {clientId} disconnected from game");
            // Handle player leaving logic
        }
        
        public void SetGameState(GameState newState)
        {
            if (IsServer)
            {
                currentGameState = newState;
                OnGameStateChanged(newState);
            }
        }
        
        private void OnGameStateChanged(GameState newState)
        {
            Debug.Log($"Game state changed to: {newState}");
            // Broadcast state change to clients
        }
    }
    
    public enum GameState
    {
        WaitingForPlayers,
        GameStarting,
        PlayerTurn,
        RollingDice,
        ChoosingTiles,
        GameEnding,
        GameFinished
    }
}