using UnityEngine;

namespace ShutTheSquare.Core
{
    public class GameSetup : MonoBehaviour
    {
        private void Start()
        {
            SetupGame();
        }
        
        private void SetupGame()
        {
            // Create GameManager
            if (GameManager.Instance == null)
            {
                GameObject gmObj = new GameObject("GameManager");
                gmObj.AddComponent<GameManager>();
                Debug.Log("GameManager created");
            }
            
            // Create BoardManager
            if (BoardManager.Instance == null)
            {
                GameObject bmObj = new GameObject("BoardManager");
                bmObj.AddComponent<BoardManager>();
                Debug.Log("BoardManager created");
            }
            
            // Create DiceManager
            if (DiceManager.Instance == null)
            {
                GameObject dmObj = new GameObject("DiceManager");
                dmObj.AddComponent<DiceManager>();
                Debug.Log("DiceManager created");
            }
            
            // Create MobileInputDetector
            var inputDetector = FindObjectOfType<ShutTheSquare.Utilities.MobileInputDetector>();
            if (inputDetector == null)
            {
                GameObject inputObj = new GameObject("MobileInputDetector");
                inputObj.AddComponent<ShutTheSquare.Utilities.MobileInputDetector>();
                Debug.Log("MobileInputDetector created");
            }
            
            // Create GameController
            var gameController = FindObjectOfType<GameController>();
            if (gameController == null)
            {
                GameObject controllerObj = new GameObject("GameController");
                controllerObj.AddComponent<GameController>();
                Debug.Log("GameController created");
            }
            
            Debug.Log("Game setup complete! Press SPACEBAR to roll dice.");
        }
    }
}