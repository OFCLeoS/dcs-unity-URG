# AI-01 - Enemy AI Specifications
## Movement
Enemies will navigate the map by using a combination of Unity's built-in NavMesh Agent Component and a custom made AI Movement Module
### Pathfinding
Pathfinding will be handled by Unity's NavMesh Agent Component
### Movement Module
A Movement Module script will be used to give an Agent movement commands and perform custom behaviour upon the receival of said commands.
### Follow Module
A Follow Module script will be used to make it so that an Agent is able to follow any Transform (e.g. the player, or an objective).
## Attacking
Enemies will attack by using our Attack System (same as the player, see SPEC_AT-01-AttackingSystem).
## Health
Enemies will be "DamageableEntities", and will use their system in order to take damage.
## Behaviour
Behaviour will be implemented using Unity's Behaviour Package, and using their Behaviour Trees. A single tree will be used for all agents. They will act depending on their current Mission Enum.