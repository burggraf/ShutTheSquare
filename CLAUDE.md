# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is "Shut the Square", a Unity mobile game project that adapts the classic "Shut the Box" dice game into a 4-player mobile experience for iOS and Android. The game features a unique 4-sided square board where each player controls one side.

## Unity Configuration

- **Unity Version**: 6000.1.14f1 (Unity 6.1)
- **Render Pipeline**: Universal Render Pipeline (URP) for mobile optimization
- **Target Platforms**: iOS 12.0+ and Android API 24+
- **Performance Target**: 60 FPS on target devices

## Development Commands

### Building the Project
- Open project in Unity 6.1 editor at `ShutTheSquare/`
- Build for iOS: File → Build Settings → iOS → Build
- Build for Android: File → Build Settings → Android → Build

### Testing
- **Desktop Testing**: Use spacebar for shake gesture, mouse for touch input
- **Mobile Testing**: Deploy to iOS/Android devices for accelerometer and touch testing
- **Multiplayer Testing**: Test locally using Unity's ParrelSync for multiple editor instances

### Package Management
- Unity packages managed via `ShutTheSquare/Packages/manifest.json`
- Key multiplayer packages: Unity Netcode, Unity Services (Relay, Authentication)

## Project Architecture

### Core Systems
- **GameManager** (`Assets/Scripts/Core/GameManager.cs`): Central singleton managing game state and networking
- **GameConstants** (`Assets/Scripts/Core/GameConstants.cs`): Centralized configuration constants
- **MobileInputDetector** (`Assets/Scripts/Utilities/MobileInputDetector.cs`): Cross-platform input handling

### Folder Structure
```
Assets/
├── Scripts/
│   ├── Core/           # Game logic and state management
│   ├── Networking/     # Multiplayer networking code
│   ├── UI/             # User interface controllers
│   └── Utilities/      # Helper classes and mobile input
├── Prefabs/
│   ├── Game/           # Game objects (dice, board, tiles)
│   ├── UI/             # UI prefabs
│   └── Network/        # Network-enabled objects
├── Scenes/
│   ├── Game/           # Gameplay scenes (MainGame.unity)
│   ├── Menus/          # Menu and navigation scenes
│   └── Testing/        # Development test scenes
├── Materials/          # 3D materials and shaders
├── Textures/           # Texture assets
└── Audio/              # Sound effects and music
```

### Network Architecture
- **Framework**: Unity Netcode for GameObjects with client-server model
- **Backend**: Unity Gaming Services (Authentication, Relay, Matchmaker)
- **State Management**: Server-authoritative with client prediction
- **Max Players**: 4 players per game session

### Mobile Optimization
- **Frame Rate**: Locked to 60 FPS via `Application.targetFrameRate`
- **Graphics**: 2x MSAA on mobile, vSync disabled for performance
- **Input**: Accelerometer-based shake detection for dice rolling
- **Power**: Screen sleep disabled during gameplay

### Game State Management
Game states handled by `GameManager.GameState` enum:
- `WaitingForPlayers` → `GameStarting` → `PlayerTurn` → `RollingDice` → `ChoosingTiles` → `GameEnding` → `GameFinished`

### Mobile Input System
- **Shake Gesture**: Accelerometer threshold detection for dice rolling
- **Touch Input**: Native touch handling with desktop mouse fallback
- **Cross-Platform**: Conditional compilation for mobile vs. desktop builds

## Key Constants
- `MAX_PLAYERS`: 4 players maximum
- `TARGET_FRAMERATE`: 60 FPS for mobile performance
- `SHAKE_THRESHOLD`: 2.0f for accelerometer sensitivity
- `NETWORK_UPDATE_RATE`: 30Hz for multiplayer synchronization

## Development Notes

### Mobile-First Approach
- All interactions designed for touch input with gesture controls
- Performance optimized for iPhone 6s/equivalent Android devices
- Cross-platform testing required for accelerometer features

### Multiplayer Considerations
- Server-side validation for all game moves and dice rolls
- Network latency target: <100ms for optimal experience
- Offline mode support for single-player against AI

### Platform-Specific Code
Use conditional compilation directives:
- `#if UNITY_ANDROID || UNITY_IOS` for mobile-specific features
- `#else` blocks provide desktop testing alternatives (spacebar for shake, mouse for touch)

## Testing Strategy
- **Unity Editor**: Primary development and desktop testing
- **Device Testing**: Regular testing on iOS and Android for mobile-specific features
- **Network Testing**: Local and remote multiplayer validation
- **Performance Testing**: Frame rate and memory usage monitoring on target devices