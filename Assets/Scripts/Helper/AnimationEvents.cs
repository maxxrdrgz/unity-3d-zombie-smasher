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

    void ResetShooting(){
        pController.canShoot = true;
        anim.Play("Idle");
    }

    void CameraStartGame(){
        SceneManager.LoadScene("Gameplay");
    }
}
