# VR Rocket and sky Hologram

Name: Simon Kennedy

Student Number: C19496436

Class Group: DT211c / TU857

Video: 

## Descripotion of the Project
For this project, I createed a VR retro-styled SSTO 3 finned rocket ship that will take off from the groud , fly around in a circle and then hover in the air. While hovering, it will move up and down and rotate slightly, while projecting a night sky that shifts from blue to purple and fly's past multi-colored stars. The player has several asteroids around them that they can pick up and throw at the ship. If the asteroids hit the sky, they will stick in place. If the ship detects that it is about to collide with an asteroid, it will attempt to dodge out of the way. If it is actually hit however, it will retract the sky and stars, and crash into the ground. 

## Instructions for use
To start the program, first press the trigger on the left controller, then either grip buttons. This will make the rocket start to accend into the air and begin the sky projection. To throw the asteroids, simply grab them using the grip buttons on the controller, "throw" the asteroid, and release the grip as you would release the grip when throwing an object in reality. To return the asteroids to you, press the right trigger. To land the ship, press the left trigger. If the ship has crashed and hit the ground, you can also hit the left trigger to reset its position and launch it again. To launch it again, press the grip button once again once it is upright
### Known issues

* If an asteroid is thrown too fast, it will travel through the sky plane
* After a re-launch, if the grip or left trigger is pressed before it has begun to project the sky plane, it will retract to a middling positon and remain stuck. It will also cause the launch sound effect to play whenever the grip is pressed instead of only at launch
* The project was initially developed using the Valve index and passthrough on SteamVR. During the demo, it was discovered there are issues building to Oculus devices, but should work OK through Oculus passthrough.  

## How it works:

## Classes and assets

| Class/asset | Source |
| ----------- | ------ |
| asteroidStick.cs | Self Written |
| audioManager.cs | from [refrence](https://www.youtube.com/watch?v=6OT43pvUyfY) |
| Colission.cs | Self Written |
| CreateGizmos.cs | Self Written |
| leftHandLaunch.cs | modified from [refrence] (https://www.youtube.com/watch?v=u6Rlr2021vw) |
| rightHandReturn.cs | modified from [refrence] (https://www.youtube.com/watch?v=u6Rlr2021vw) |
| rocket.cs | Self Written |
| sound.cs | from [refrence] (https://www.youtube.com/watch?v=6OT43pvUyfY) |

## Refrences 
* https://www.youtube.com/watch?v=6OT43pvUyfY
* https://www.youtube.com/watch?v=u6Rlr2021vw
* https://pixabay.com/sound-effects/search/rocket/ 
* https://www.youtube.com/watch?v=ztz7n6jXmlQ
* https://www.youtube.com/watch?v=Msx3v-_WDss
 
# What I am most proud of in the assignment
The Launch sequence where the rocket performs a spin in a circlular ring was the most challenging component to get working correctly. Most rotate funcions attempt to get the object to rotate its forward vector to the destination. The problem is that the "forward" of the rocket model is one of its fins, not the top of the ship, meaning it would fly sideways rather than straight. The solution was to the Quaternion.FromToRotation method using the objects up vector to point in a direction. The code looks as follows:

```
cs
{
    Vector3 directon = waypoints[currentWaypoint].transform.position - transform.position;
    Quaternion toRotate = Quaternion.FromToRotation(Vector3.up, directon);
    transform.rotation = toRotate;
}
```
