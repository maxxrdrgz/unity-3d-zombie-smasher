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

    /**
        Deactivates the bullet game object
    */
    void DeactivateGameObject(){
        gameObject.SetActive(false);
    }

    /**
        Detects collision with gameobjects with the obstacle tag. If so,
        the bullet gets deactivated.

        @param {Collision} The other Collider2D involved in this collision
    */
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Obstacle"){
            gameObject.SetActive(false);
        }    
    }
}
