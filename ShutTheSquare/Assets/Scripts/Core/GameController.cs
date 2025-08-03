using UnityEngine;
using ShutTheSquare.Utilities;

namespace ShutTheSquare.Core
{
    public class GameController : MonoBehaviour
    {
        private MobileInputDetector inputDetector;
        private GameManager gameManager;
        
        private void Start()
        {
            inputDetector = FindObjectOfType<MobileInputDetector>();
            gameManager = FindObjectOfType<GameManager>();
            
            if (inputDetector != null)
            {
                inputDetector.OnShakeDetected += OnShakeDetected;
            }
        }
        
        private void Update()
        {
            // Desktop testing - spacebar to roll dice
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("SPACEBAR PRESSED!");
                OnShakeDetected();
            }
        }
        
        private void OnShakeDetected()
        {
            Debug.Log("Shake detected - trying to roll dice");
            
            // Try direct approach first
            var diceManager = FindObjectOfType<DiceManager>();
            if (diceManager != null)
            {
                Debug.Log("Found DiceManager - rolling dice directly");
                diceManager.RollDice();
            }
            else
            {
                Debug.LogError("DiceManager not found!");
            }
            
            // Also try through GameManager
            if (gameManager != null && gameManager.CanRollDice())
            {
                Debug.Log("Also trying through GameManager");
                gameManager.RollDice();
            }
            else
            {
                Debug.LogError("GameManager not available or can't roll dice");
            }
        }
        
        private void OnDestroy()
        {
            if (inputDetector != null)
            {
                inputDetector.OnShakeDetected -= OnShakeDetected;
            }
        }
    }
}