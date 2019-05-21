using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlock : MonoBehaviour
{
    public Transform otherBlock;
    public float halfLength = 100f;

    private Transform player;
    private float endoffset = 10f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveGround();
    }

    void MoveGround(){
        if(transform.position.z + halfLength < 
           player.transform.position.z - endoffset){
            
            transform.position = new Vector3(
                otherBlock.position.x, 
                otherBlock.position.y, 
                otherBlock.position.z + halfLength * 2
            );
        }
    }
}
