using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard_Player_Controller : MonoBehaviour
{
    public float MovementSpeed = 6f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRb;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
    }
	
	void FixedUpdate ()
    {
        //getting the input axis for both the horezontal and vertical
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
	}

    void Move(float h, float v)
    {
        //setting the value of "move" to the horezontal & vertical axis
        movement.Set(h, 0f, v);
        //making sure that the player moves at the same speed even if the player uses h&v, then multiplaying it by speed, then multiplaying it by time.deltatime to make sure that it happens with normal time
        movement = movement.normalized * MovementSpeed * Time.deltaTime;
        //adding the movement to the rigidbody
        playerRb.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        //casting a ray from the camera
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        // used to store whatever the ray hit
        RaycastHit floorHit;
        // a buit in unity function that is used as a bool, trye if the conditions are met
        //the firs parameter is the ray, the second is an out that stores the out info in a variable, the third is the length of the ray, the last is the layer that the ray interacts with
        if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRb.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
}
