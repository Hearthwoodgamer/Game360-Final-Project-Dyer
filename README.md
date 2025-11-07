# Game360-Final-Project
# Task 3: Complete Patterns Integration

# Project Evolution
# Task 2 Skills Foundation 
- Singleton Pattern: GameManager, AudioManager
- Basic game with centralized management

## Task 3 Additions
## Observer Pattern
- EventManager for decoupled communication
- Events implemented:
  OnScoreChanged
  OnPlayerStateChanged
  OnGameOver
  OnLevelComplete
  OnCoinCollected
  PauseGame
  ResumeGame
  OnEnemyDefeated
  OnPowerUpCollected
  
- Observers: UIManager, Enemy, PlayerController, GameManager, AudioManager, Collectible

## State Machine Pattern
- Player States: 
Idle, Moving, Jumping
- Game States: Enhanced from Task 2
- State transitions: [Describe key transitions]
Space is used to switch to the jump state, and the A and D keys are used to control horizontal moevement and change the state to moving. 

### Key Integration Points
1. Score System: Singleton → Observer → UI
2. Player Actions: Input → State → Event → Audio
3. Game Flow: GameState → Events → Scene Changes

## Repository Statistics
- Total Commits: 23
- Task 3 Commits: 23
- Lines of Code: ~500-1000
- Development Time: 10

## How to Play
- Controls: 
A = left
D = right
Esc = pause
Space = jump
F = shoot
- Objective: reach the end flag
- Optional objectives
  Kill all enemies
  Collect all cherries
  Get 100 score
- New Features: Music and sounds, player movement, platforming, sidescroller camera.
