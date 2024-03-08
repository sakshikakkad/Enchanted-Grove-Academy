# Enchanted-Grove-Academy
A 3D adventure game created in Unity

**To utilize your ThirdPersonCamera script effectively:**

1. Attach the Script to Your Main Camera:

2. Configure the Target and Desired Pose
    *Target*: This should be the Transform of your main character. The camera will use this to determine its rotation so that it always faces the character.

    *Desired Pose*: This should be a Transform that represents the desired position and orientation of the camera relative to the target. You can create an empty GameObject as a child of your main character, position it where you want the camera to be relative to the character, and then assign this GameObject's Transform to the desiredPose field in the script.

3. Set Smooth Time and Max Spped 
    positionSmoothTime = 0.25
    rotationSmoothTime = 0.5
    positionMaxSpeed = 1000
    rotationMaxSpeed = 1000




