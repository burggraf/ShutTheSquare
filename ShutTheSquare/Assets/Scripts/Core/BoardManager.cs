using UnityEngine;

namespace ShutTheSquare.Core
{
    public class BoardManager : MonoBehaviour
    {
        public static BoardManager Instance { get; private set; }
        
        public enum PlayerSide
        {
            North, East, South, West
        }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public bool ShutTile(PlayerSide player, int tileNumber)
        {
            Debug.Log($"Player {player} shut tile {tileNumber}");
            return true;
        }
        
        public bool CheckWinCondition(PlayerSide player)
        {
            Debug.Log($"Checking win condition for {player}");
            return false;
        }
        
        public System.Collections.Generic.List<int> GetValidCombinations(PlayerSide player, int total)
        {
            Debug.Log($"Getting valid combinations for {player} with total {total}");
            return new System.Collections.Generic.List<int> { total };
        }
    }
}