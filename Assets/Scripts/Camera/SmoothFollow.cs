using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 6.3f;
    public float height = 3.5f;
    public float height_damping = 3.25f;
    public float rotation_damping = 0.27f;



    // Start is called before the first frame update
    void Start()
    {
       target = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    // Called at the end of the frame
    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer(){
        float wanted_rotation_angle = target.eulerAngles.y;
        float wanted_height = target.position.y + height;

        float current_rotaion_angle = transform.eulerAngles.y;
        float current_height = transform.position.y;

        current_rotaion_angle = Mathf.LerpAngle(
            current_rotaion_angle, 
            wanted_rotation_angle, 
            rotation_damping * Time.deltaTime
        );

        current_height = Mathf.Lerp(
            current_height, 
            wanted_height, 
            height_damping * Time.deltaTime
        );

        Quaternion current_rotation = Quaternion.Euler(
            0f, 
            current_rotaion_angle, 
            0f
        );

        transform.position = target.position;
        transform.position -= current_rotation * Vector3.forward * distance;
        transform.position = new Vector3(
            transform.position.x, 
            current_height, 
            transform.position.z
        );

    }
}
