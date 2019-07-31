using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public Animator cameraAnim;

    /**
        Tells the camera's animator to play the slide animation.
    */
    public void PlayGame(){
        cameraAnim.Play("Slide");
    }
}
