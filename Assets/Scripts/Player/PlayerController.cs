using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Rigidbody rbody;


    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovementWithKeyboard();
        ChangeRotation();
    }
    
    private void FixedUpdate() {
        MoveTank();
    }

    void MoveTank(){
        rbody.MovePosition(rbody.position + speed * Time.deltaTime);
    }

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
}
