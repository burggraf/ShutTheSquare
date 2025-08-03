# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is "Shut the Square", a Unity mobile game project that adapts the classic "Shut the Box" dice game into a 4-player mobile experience for iOS and Android. The game features a unique 4-sided square board where each player controls one side.

## Unity Configuration

- **Unity Version**: 2022.3 LTS (REQUIRED - Do not use Unity 6.1 or other versions)
- **Render Pipeline**: Universal Render Pipeline (URP) for mobile optimization
- **Target Platforms**: iOS 12.0+ and Android API 24+
- **Performance Target**: 60 FPS on target devices

## Development Commands

### Building the Project
- Open project in Unity 2022.3 LTS editor at `ShutTheSquare/`
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

## Available Specialized Agents

The following specialized agents are available in `.claude/agents/` to assist with different aspects of development:

### 1. Unity Game Developer (`unity-game-developer.md`)
**Purpose**: Core Unity game development and C# scripting
**Use For**: 
- Implementing game mechanics (dice rolling, tile selection, scoring)
- Unity component architecture and scene management
- Game state systems and singleton patterns
- Unity prefab systems and asset organization
- UI implementation and Unity-specific debugging

### 2. Mobile Performance Specialist (`mobile-performance-specialist.md`)
**Purpose**: Mobile optimization and platform-specific features
**Use For**:
- iOS/Android performance optimization and 60 FPS maintenance
- Mobile input systems (accelerometer shake detection, touch controls)
- Cross-platform development patterns and conditional compilation
- Battery optimization and graphics settings
- Platform-specific build configurations

### 3. Multiplayer Network Specialist (`multiplayer-network-specialist.md`)
**Purpose**: Unity Netcode and multiplayer networking
**Use For**:
- Unity Netcode for GameObjects implementation
- Server-authoritative networking with client prediction
- Unity Gaming Services integration (Authentication, Relay, Matchmaker)
- Network state synchronization and RPC systems
- Mobile networking optimization and connection management

### 4. Mobile Testing Specialist (`mobile-testing-specialist.md`)
**Purpose**: Cross-platform testing and validation
**Use For**:
- iOS/Android compatibility testing and device validation
- Performance testing and profiling on mobile devices
- Accelerometer and touch input testing across devices
- Multiplayer testing using ParrelSync and device deployment
- Build validation and deployment process verification

## Development Plan & Progress Tracking

### Master Development Plan
**Primary Reference**: `Development_Plan_Checklist.md` - Comprehensive 24-week development plan with phase-by-phase milestones, agent coordination strategy, and progress tracking.

**Key Phases**:
- **Phase 1 (Weeks 1-4)**: Core Foundation - Single player game loop
- **Phase 2 (Weeks 5-8)**: Local Multiplayer - 4-player local game  
- **Phase 3 (Weeks 9-12)**: Network Foundation - Online multiplayer
- **Phase 4 (Weeks 13-16)**: Platform Ready - Mobile optimization
- **Phase 5 (Weeks 17-24)**: Advanced Features - Polish and launch

### Progress Tracking
- **Weekly Reviews**: Check Development_Plan_Checklist.md every Monday for progress assessment
- **Go/No-Go Checkpoints**: Week 4, 8, 12, 16 with defined success criteria
- **Risk Monitoring**: Timeline, technical, and resource risk assessment

**Current Status**: Foundation Phase (Week 1) - Setting up Unity packages and core game mechanics

## Agent Usage Guidelines

### When to Use Agents
- **Proactively**: Agents will engage automatically for their areas of expertise
- **On-demand**: Request specific agents by mentioning their domain (e.g., "mobile performance", "networking", "Unity development")
- **Code Reviews**: Agents will provide specialized feedback during code reviews

### Agent Coordination
- Agents work together on complex features that span multiple domains
- Unity Game Developer handles core implementation
- Mobile Performance Specialist optimizes for mobile platforms
- Network Specialist adds multiplayer functionality
- Testing Specialist validates across platforms and devices

### Phase-Based Agent Priority
Refer to `Development_Plan_Checklist.md` for detailed agent coordination strategy by development phase.