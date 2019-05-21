using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rbody;    

    public void Move(float speed){
        rbody.AddForce(transform.forward.normalized * speed);
        Invoke("DeactivateGameObject", 5f);
    }

    void DeactivateGameObject(){
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Obstacle"){
            gameObject.SetActive(false);
        }    
    }
}
