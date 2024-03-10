# Enchanted-Grove-Academy
A 3D adventure game created in Unity for CS 4455 (Video Game Design)
Contributers: Sakshi Kakkad, Alina Polyudova, Alex Soong, Melissa Leng, Leila Baniassad

# Start Scene
The Start Scene file is located at Assets -> Scenes -> Start

# How to Play
The game begins at the Start scene, where after pressing "Start Game" will take you to the Main Scene.

The Main scene is the main map of the game, from which you access minigames and quests. We currently have one minigame (gardening) which can be found by following the path in the Main scene and walking up to the fence.

The goal of the game is to finish fairy school by completing minigames and quests and earning fairy dust. Each time you play the gardening game you earn fairy dust, which eventually unlocks the spider quest.

Once the quest is unlocked, you will have to make it past the AI spiders and escape to win the game!

# Requirements
Overall
- Implemented in Unity, consists of a 3D world, 3rd person main character with animations
- Game Feel with background audio, URP post processing, responsive controls and animations
- Provides interesting/new experiences with randomization scripts in minigames/quests 
- GUI Menus for pausing, instructions, scene navigation, etc

Quest
- Implement real-time steering, path planning, state-machine based AI with animations
- Unlockable scene

Gardening Class
- Utilizes rigid body physics simulation with interactive objects

# Known Problem Areas
- AI Spider animation rotation issues
- Minor issues with player movement/rotation by external physics

# Team Contribution Manifest
- For the programming requirement, each C# script is commented with author and contribution info (if multiple people worked on the same script)
- To view all scripts, go to Assets -> Scripts
- Full breakdown of tasks can be found on our team Trello -> https://trello.com/w/enchantedpixelstudios

-------------------------------------------------------------------------

Sakshi - project management, github merging, scene setup (assets, terrain, post processing) in all scenes, main character creation and animations in Blender and Unity, bug fixes in all scenes, Player Controller, Quest Manager for random AI enemy spawning, scene navigation script (main to garden)
Alina - Spider integration in quest scene with animations, AI Controller and state machine, NavMesh setup, Quest scene functionality and UI (win/lose conditions), Quest UI in Main scene
Alex - Scene setup (quest), Player Controller, Start scene setup and UI, scene navigation (main to quest), Pause UI
Leila - Third person camera movement, Pixie dust UI and functionality, bug fixes in gardening scene
Melissa - Garden scene setup (crop prefabs), Crop Collection and Crop scripts, Pause and Quit UI, Garden scene functionality and UI (win/lose conditions), Garden scene timer, add to inventory UI and functionality

