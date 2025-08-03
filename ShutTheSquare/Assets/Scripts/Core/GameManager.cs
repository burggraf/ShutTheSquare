using UnityEngine;
using System;

namespace ShutTheSquare.Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game State")]
        [SerializeField] private GameState currentGameState = GameState.WaitingForPlayers;
        [SerializeField] private BoardManager.PlayerSide currentPlayerTurn = BoardManager.PlayerSide.North;
        
        [Header("Mobile Optimization")]
        [SerializeField] private bool enableMobileOptimizations = true;
        
        // Singleton instance
        public static GameManager Instance { get; private set; }
        
        // Events
        public event Action<GameState> OnGameStateChanged;
        public event Action<BoardManager.PlayerSide> OnPlayerTurnChanged;
        
        // Component references
        private BoardManager boardManager;
        private DiceManager diceManager;
        
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
        
        private void Start()
        {
            InitializeGameComponents();
            SetGameState(GameState.GameStarting);
        }
        
        private void InitializeMobileSettings()
        {
            if (enableMobileOptimizations)
            {
                Application.targetFrameRate = GameConstants.TARGET_FRAMERATE;
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
                QualitySettings.vSyncCount = 0;
                
#if UNITY_ANDROID || UNITY_IOS
                QualitySettings.antiAliasing = 2;
#endif
            }
        }
        
        private void InitializeGameComponents()
        {
            boardManager = FindObjectOfType<BoardManager>();
            diceManager = FindObjectOfType<DiceManager>();
            
            if (diceManager != null)
            {
                diceManager.OnDiceRollComplete += OnDiceRollComplete;
                diceManager.OnDiceRollStarted += OnDiceRollStarted;
            }
        }
        
        public void StartGame()
        {
            SetGameState(GameState.GameStarting);
            StartNewRound();
        }
        
        private void StartNewRound()
        {
            SetGameState(GameState.PlayerTurn);
            OnPlayerTurnChanged?.Invoke(currentPlayerTurn);
        }
        
        public void RollDice()
        {
            if (currentGameState == GameState.PlayerTurn && diceManager != null)
            {
                SetGameState(GameState.RollingDice);
                diceManager.RollDice();
            }
        }
        
        private void OnDiceRollStarted()
        {
            Debug.Log("Dice rolling started");
        }
        
        private void OnDiceRollComplete(int die1Value, int die2Value)
        {
            int totalValue = die1Value + die2Value;
            Debug.Log($"Dice roll complete: {die1Value} + {die2Value} = {totalValue}");
            
            if (boardManager != null)
            {
                var availableCombinations = boardManager.GetValidCombinations(currentPlayerTurn, totalValue);
                if (availableCombinations.Count > 0)
                {
                    SetGameState(GameState.ChoosingTiles);
                }
                else
                {
                    Debug.Log("No valid moves available - turn ending");
                    EndPlayerTurn();
                }
            }
        }
        
        public void SelectTile(int tileNumber)
        {
            if (currentGameState == GameState.ChoosingTiles && boardManager != null)
            {
                bool success = boardManager.ShutTile(currentPlayerTurn, tileNumber);
                if (success)
                {
                    if (boardManager.CheckWinCondition(currentPlayerTurn))
                    {
                        SetGameState(GameState.GameFinished);
                        Debug.Log($"Player {currentPlayerTurn} wins!");
                    }
                    else
                    {
                        EndPlayerTurn();
                    }
                }
            }
        }
        
        private void EndPlayerTurn()
        {
            SetGameState(GameState.PlayerTurn);
            OnPlayerTurnChanged?.Invoke(currentPlayerTurn);
        }
        
        public void SetGameState(GameState newState)
        {
            currentGameState = newState;
            OnGameStateChanged?.Invoke(newState);
            Debug.Log($"Game state changed to: {newState}");
        }
        
        public GameState GetCurrentGameState()
        {
            return currentGameState;
        }
        
        public BoardManager.PlayerSide GetCurrentPlayer()
        {
            return currentPlayerTurn;
        }
        
        public bool CanRollDice()
        {
            return currentGameState == GameState.PlayerTurn && 
                   (diceManager == null || !diceManager.IsRolling);
        }
        
        private void OnDestroy()
        {
            if (diceManager != null)
            {
                diceManager.OnDiceRollComplete -= OnDiceRollComplete;
                diceManager.OnDiceRollStarted -= OnDiceRollStarted;
            }
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