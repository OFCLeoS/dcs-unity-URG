# WAV-01 - Wave System Requirements
## Overview
The game must contain a Wave System that will incrementally increase it's difficulty as well as decide what the player's objective for that wave is.
## Requirements
### Wave Event Order
A Wave must follow the following format:
1. Preparation Time
2. Wave Content
3. Player Either Fails Wave, Dies, or Completes Wave
4. If Player is alive, they must go back to the elevator
### Difficulty Scaling
The Wave System must be able to scale the difficulty of the game. This must be done, for example, by modifying the amount of enemies that are spawned, their health, damage, and other attributes
### Wave Objectives
Waves should contain different types of objectives that must be completed in a certain amount of time.
#### Defend Objectives
In Defend Objectives, the Player must defend a certain object from enemy attacks. Spawned enemies should prioritize attacking the objective, and only attack the player if certain conditions are met.