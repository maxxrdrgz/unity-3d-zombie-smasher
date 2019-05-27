using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Animator cameraAnim;

    public void PlayGame(){
        cameraAnim.Play("Slide");
    }
}
