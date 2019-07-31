using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timer = 3f;

    /**
        Deactivates the game object this script is attached to.
    */
    void DeactivateGameObject(){
        gameObject.SetActive(false);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeactivateGameObject", timer);
    }
}
