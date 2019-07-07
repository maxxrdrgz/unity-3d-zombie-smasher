using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvents : MonoBehaviour
{
    private PlayerController pController;
    private Animator anim;

    private void Start() {
        pController = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<PlayerController>();
        anim = gameObject.GetComponent<Animator>();
    }

    /**
        Resets the canshoot boolean found in the player controller and plays the
        idle animation for the player game object
    */
    void ResetShooting(){
        pController.canShoot = true;
        anim.Play("Idle");
    }

    /**
        Loads the gameplay scene. This is called when the camera finishes it's
        start up animation.
    */
    void CameraStartGame(){
        SceneManager.LoadScene("Gameplay");
    }
}
