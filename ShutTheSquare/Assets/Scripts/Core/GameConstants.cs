using UnityEngine;

namespace ShutTheSquare.Core
{
    public static class GameConstants
    {
        // Game Configuration
        public const int MAX_PLAYERS = 4;
        public const int BOARD_SIZE = 4; // 4x4 grid
        public const int DICE_COUNT = 2;
        
        // Mobile Performance Settings
        public const int TARGET_FRAMERATE = 60;
        public const float TARGET_DELTA_TIME = 1f / TARGET_FRAMERATE;
        
        // Networking
        public const float NETWORK_UPDATE_RATE = 30f;
        public const int MAX_NETWORK_LATENCY_MS = 100;
        
        // Game Rules
        public const int INITIAL_TILES_COUNT = 9; // Numbers 1-9 initially available
        public const int WIN_CONDITION_TILES_SHUT = 7; // Need to shut 7+ tiles to win
        
        // UI Constants
        public const float TOUCH_SENSITIVITY = 1.0f;
        public const float SHAKE_THRESHOLD = 2.0f; // For shake-to-roll gesture
        
        // Audio
        public const float DEFAULT_SFX_VOLUME = 0.7f;
        public const float DEFAULT_MUSIC_VOLUME = 0.5f;
    }
}