using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

    // Made with the help of the tutorial by Brackeys that can be found at https://www.youtube.com/watch?v=0HwZQt94uHQ

    public Texture2D fadeTexture; // The texture that will overlay the screen. This can be a black image or a loading graphic
    public float fadeSpeed;

    private int drawDepth = -1000; // Used to insure the fade image is drawn over everything else in the scene
    private float alpha = 1.0f;
    private int fadeDirection = -1; // used to dertermine wether scene is fading in or out. -1 = in and 1 = out

    private void OnGUI()
    {
        // fade in or out the alpha value based on the direction, speed and Time.DeltaTime to convert the operation to seconds
        alpha += fadeDirection * fadeSpeed * Time.deltaTime;
        // clamp value because alpha can only be between 0 and 1
        alpha = Mathf.Clamp(alpha, 0, 1);

        // set colour of GUI in this case texture. All colour values stay the same other than the alpha, which is out target
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha); // setting the alpha
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture); // Draw the texture to fill the entire screen
    }

    // Set the fade direction based on the the fadeDirection variable. -1 = in and 1 = out 
    public float BeginFade(int direction) // float is used here so we can return the speed so the calling object can no how long it will take to finish fading
    {
        fadeDirection = direction;
        return fadeSpeed;
    }

    //This creates a fade in effect when the level is loaded
    private void OnLevelWasLoaded(int level)
    {
        BeginFade(-1);
    }
}
