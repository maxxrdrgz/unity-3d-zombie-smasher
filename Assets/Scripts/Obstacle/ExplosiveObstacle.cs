using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObstacle : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int damage = 20;
    
    /**
        Detects if this game object has collided with either the player or the
        bullet game object. Instantiates an explosion and if a collision with
        the player has been detected, then call the apply damage function.
        Lastly, disable the game object this script is attached to.

        @param {Collision} The Collision data associated with this collision 
        event.
    */
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            Instantiate(
                explosionPrefab,
                transform.position,
                Quaternion.identity
            );
            other.gameObject.GetComponent<PlayerHealth>().ApplyDamage(damage);
            gameObject.SetActive(false);
        }

        if(other.gameObject.tag == "Bullet"){
            Instantiate(
                explosionPrefab,
                transform.position,
                Quaternion.identity
            );
            gameObject.SetActive(false);
        }
    }
}
