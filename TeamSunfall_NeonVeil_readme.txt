Team Sunfall - Neon Veil

The start scene is "TitleScreen"

The game is played by manuevering the player through the level from the start point to the green orb which marks the end point. The player can be moved with arrow keys or wasd. Sprint is activated with the shift key. The E button deploys a hologram. F interacts with the environment.

Throughout the level, the player comes across technological obstacles which must be overcome. Towards the beginning, there is a path with red lasers throughout it, and one facing down it. If any of these are triggered by the player it notifies an enemy to investigate. This enemy has a vision radius which is viewable by the player. If the player is within this cone, the enemy will engage and will kill the player if contact is made, ending the game. If the player escapes from the cone, the enemy will return to its patrolling route. To advance through the hallway, the player must use a nearby cube to block the laser pointing down the hallway. The player must then also use timing to traverse through the intermittent lasers going across. Past this, the player can press a button to open a future door. There are also security cameras which must be avoided in order for the enemy to not be summoned. The player still comes across patrolling enemies, which can be distracted by deploying a hologram. The player can then move on to press another button and open the door to the platform containing the objective orb.

We primarily ran into issues in implementing our player animation when accounting for a camera. We were able to create a BlendTree root motion character, however its rotation logic was not compatible with what we had in mind for our camera control by the user, so our current version does not have player rotation animation well implemented. We also think that work can be done to make the level feel more cohesive and realistic aesthetically, and plan to incorporate more audio cues and potentially music for an immersive experience.

Manifest:

Zach Slaton
- Hologram (HologramController.cs)
- 3rd Person Camera Control (ThirdPersonCamera.cs)
- Doors (DoorController.cs)
- Button Interactivity (DoorButtonController.cs, InteractableIndicator.cs)
- Information Monitor (MoniorController.cs, MoniorTrigger.cs)
- Vision Cones (VisionCone.cs)

Luting Wang
- Enemy Model
- Enemy Animation (EnemyController.cs, Animation.cs)
- Player Controller (PlayerController.cs)
- Level Assets

William Greene
- Lasers (LevelLaser.cs)
- Security Cameras (CameraDetection.cs)
- Enemy Alarm (EnemyController.cs)
- UI Scripts (GameQuitter.cs, GameStarter.cs, PauseMenuToggle.cs)

Sagar Singhal
- Player Model 
- Player Animation (ExoGray_Controller, PlayerController.cs)
- Enemy State Machine AI / NavMesh (EnemyController.cs)
- End Condition Orb (EndCollision.cs)

