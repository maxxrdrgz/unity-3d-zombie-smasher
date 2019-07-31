using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public GameObject bloodFX_prefab;
    private float speed;

    private Rigidbody rbody;
    private bool is_alive;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        speed = Random.Range(3f, 6f);
        is_alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(is_alive){
            rbody.velocity = new Vector3(0f, 0f, -speed);
        }
        if(transform.position.y < -10f){
            gameObject.SetActive(false);
        }
    }
    /**
        This function stops the zombie from moving and also disables the
        collider, making the zombie fall through the floor. The rotation is
        changed to give the appearance that the zombie has been ran over and the
        scale is changed to give the appearence that the zombie has been
        smashed.
    */
    void Die(){
        is_alive = false;
        rbody.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        GetComponentInChildren<Animator>().Play("Idle");
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        transform.localScale = new Vector3(1f, 1f, 0.2f);
        transform.position = new Vector3(
            transform.position.x, 
            0.2f, 
            transform.position.z
        );
    }

    /** 
        Deactivates the gameobject this script is attached to.
    */
    void DeactivateGameobject(){
        gameObject.SetActive(false);
    }

    /** 
        This function detects if the zombie has collided with either the player
        or the bullet gameobject.!-- If so, the bloodFX prefab is instantiated,
        the zombie is deactived after a 3 second delay, the score is increased
        and finally the die() function is called to animate the zombie's death.

        @param {Collision} The Collision data associated with this collision 
        event.
    */
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Bullet"){
            Instantiate(
                bloodFX_prefab, 
                transform.position, 
                Quaternion.identity
            );
            Invoke("DeactivateGameobject", 3f);
            GameplayController.instance.IncreaseScore();
            Die();
        }
    }
}
