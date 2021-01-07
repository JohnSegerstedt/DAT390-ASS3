# DAT380-ASS3

Authors:
* Petra Béczi
* David Campos Rodrigués
* John Segerstedt

Requirement and How It is Met:
* *Unity assets*: The prototype utilizes unity assets in the form prefabs using self-made Blender 3D models and animations.
* *Player interaction*: Players can select a plant type from the shop UI, purchase a selected plant on the game board grid square, and collect sunshine.
* *Anchors*: Our prototype uses image tracking from Vuforia which anchors the game board to an image target. Additionally, purchased plants are anchored onto the game board prefab.
* *Image Tracking*: The game board is instantiated on the detection of the physical game board token.
* *Ray Casting*: Ray casting is used for detecting which grid square the player is attempting to purchases a plant onto.
* *Additional Techniques from outside the Module*: Vuforia
