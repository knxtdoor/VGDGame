Team Sunfall - Neon Veil

The start scene is "TitleScreen"


How to play:
The game is played by manuevering the player through the level from the start point to the green orb which marks the end point. 
The player can be moved with arrow keys or wasd. 
Sprint is activated with the shift key. 
The E button deploys a hologram. 
F interacts with the environment.


Game Description:
Throughout the game, the player will encounter obstacles which impede their progress. 
Robot guards patrol the levels, blocking access to sections of each level. 
Lasers trigger an alarm, which will summon a nearby guard to investigate. 
If a guard sees a player, it will persue them. Their FOV is indicated by a vision cone on the ground.
The player can press E to activate their Hologram, which will distract a guard for a short time.
The player can make use of physics-based crates to obstruct lasers and progress through the level.
There are buttons which open doors within the levels, requiring planning and stealth to successfully complete each level.
Each level contains a green orb, which will advance the player to the next level.

Technological requirements:
- The player is a humanoid skeleton with BlendTree animations. The player is always centered in the camera and can always be controlled, except in menus.
- The guards use a NavMesh to navigate their patrol routes. If they are alerted by an alarm or see a player, they will change states and instead investigate. Avoiding the guards is a core gameplay element
- The transparent crates are Physics-based RigidBodies. They must be pushed into place to avoid lasers or the line of sight of a guard.
- The levels require planning and decision making, creating interesting choices to make.
- The pause menu allows allows the player to return to the title screen or check the controls.


Problem areas:
- The character's control scheme and animations are still a bit weak. It is difficult to strafe, making crate pushing difficult.
- We were not able to include many sound effects in the game, such as footsteps or crate collisions.
- The level design is not very consistent. Some levels are tight and contained, while others are wide opened. Additionally, the environment art (like the skyboxes) is inconsistent.


Manifest:

Zach Slaton
Code:
- Hologram (HologramController.cs)
- 3rd Person Camera Control (ThirdPersonCamera.cs)
- Doors (DoorController.cs)
- Button Interactivity (DoorButtonController.cs, InteractableIndicator.cs, EndButtonController.cs)
- Information Monitor (MoniorController.cs, MoniorTrigger.cs)
- Vision Cones (VisionCone.cs)
- KillPlane (KillPlaneController.cs)
- DeathScreen/EndScreen Fade in (DeathScreen.cs)
- Credits screen (CreditsScreenController.cs)
Assets:
- 2nd Level design and layout (ZachLevel)
- Final level (gold button) design and layout (EndGame)
- Button Model + Animations
- Monitor Model
- Exclamation point model + animation
- Door model + animation
- 2nd background music song

Luting Wang
Code:
- Enemy Animation (EnemyController.cs, Animation.cs)
- Player Controller (PlayerController.cs)
- Enemy Button (EnemyButtonController.cs, EnemyButtonTriggerController.cs)
Assets:
- 1st level design and layout (Luting_map)
- Level Assets (Walls, Crates, Lamps) (3rd party)
- Enemy Model (Robot) (3rd Party)
- City skybox (3rd party)

William Greene
Code:
- Lasers (LevelLaser.cs)
- Security Cameras (CameraDetection.cs)
- Enemy Alarm (EnemyController.cs)
- UI Scripts (GameQuitter.cs, GameStarter.cs, PauseMenuToggle.cs)
Assets:
- 3rd Level design and layout (Will_map)
- Security camera model 
- Ladder model
- First background music song (3rd party)

Sagar Singhal
Code:
- Player Animation (ExoGray_Controller, PlayerController.cs)
- Enemy State Machine AI / NavMesh (EnemyController.cs)
- End Condition Orb (EndCollision.cs)
Assets:
- 4th Level design and layout (Sagar_Level)
- Player Model (3rd party)
- Hologram model (3rd party)
- End level orb (3rd party)


Special thanks to Wyatt Kopcha for making one of the background music tracks.


3rd party assets:
Level assets - Model + Material for floors,walls, pillars, lamps - https://assetstore.unity.com/packages/3d/environments/sci-fi/voxel-scifi-environment-free-version-101492
Guard model - robot orb - https://assetstore.unity.com/packages/3d/characters/robots/robot-sphere-136226
Player model - Exo Gray - Find on Unity store
End level orb - Orb asset package - Find on Unity store
Skyboxes - Vast Outer Space asset package - Find on unity store
Skyboxes - Warped City asset package - https://assetstore.unity.com/packages/2d/environments/warped-city-assets-pack-138128 
First background music track (https://freesound.org/people/Bertsz/sounds/672784/)


