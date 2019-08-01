using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : BaseController
{
    private Rigidbody rbody;
    public Transform bullet_startpoint;
    public GameObject bullet_prefab;
    public ParticleSystem shootFX;

    private Animator shootSliderAnim;

    [HideInInspector]
    public bool canShoot;

    /** 
        At the start, this function will add the ShootingControl listener to the
        shootbtn gameobject and get a few components.
    */
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        GameObject
            .Find("ShootBtn")
            .GetComponent<Button>().onClick
            .AddListener(ShootingControl);
        canShoot = true;
        shootSliderAnim = GameObject.Find("Fire Bar").GetComponent<Animator>();

    }

    void Update()
    {
        ControlMovementWithKeyboard();
        ChangeRotation();
    }
    
    private void FixedUpdate() {
        MoveTank();
    }
    
    /** 
        Moves the rigid body of the tank forward.
    */
    void MoveTank(){
        rbody.MovePosition(rbody.position + speed * Time.deltaTime);
    }

    /** 
        Get's input from the keyboard. Depeneding on the input, the appropriate
        function is called from BaseController.
    */
    void ControlMovementWithKeyboard(){
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
            MoveLeft();
        }
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
            MoveRight();
        }
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
            MoveFast();
        }
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
            MoveSlow();
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow) ||
           Input.GetKeyUp(KeyCode.A) ||
           Input.GetKeyUp(KeyCode.RightArrow) ||
           Input.GetKeyUp(KeyCode.D)){
            MoveStraight();
        }
        if(Input.GetKeyUp(KeyCode.UpArrow) || 
           Input.GetKeyUp(KeyCode.W) ||
           Input.GetKeyUp(KeyCode.DownArrow) ||
           Input.GetKeyUp(KeyCode.S)){
            MoveNormal();
        }
    }

    /** 
        This function will slightly rotate the position of the tank either
        left or right depending on the speed of x. Otherwise the tank will
        remain straight.
    */
    void ChangeRotation(){
        if(speed.x > 0){
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                Quaternion.Euler(0f, maxAngle, 0f), 
                Time.deltaTime * rotationSpeed
            );
        }else if(speed.x < 0){
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                Quaternion.Euler(0f, -maxAngle, 0f), 
                Time.deltaTime * rotationSpeed
            );
        }else{
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                Quaternion.Euler(0f, 0f, 0f), 
                Time.deltaTime * rotationSpeed
            );
        }
    }

    /** 
        This function will create the shooting effect for the tank. If the game
        is not paused, and canshoot is true, the bullet gameobject will be
        created and moved. The canshoot is reset in the AnimationEvents.cs found
        in the Helper folder.
    */
    public void ShootingControl(){
        if(Time.timeScale != 0){
            if(canShoot){
                GameObject bullet = Instantiate(bullet_prefab, bullet_startpoint.position, Quaternion.identity);
                bullet.GetComponent<BulletScript>().Move(2000f);
                shootFX.Play();
                canShoot = false;
                shootSliderAnim.Play("Fill");
            }
        }
    }
}
