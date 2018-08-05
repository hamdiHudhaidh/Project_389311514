using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float MovementSpeed = 6f;
    public float RotationSpeed = 5f;

    Vector3 movement;
    Animator anim;
    //Rigidbody playerRb;
    float animValueH;
    float animValueV;
    int intForAnim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        //playerRb = GetComponent<Rigidbody>();
    }
	
	void FixedUpdate ()
    {
        //getting the input axis for both the horezontal and vertical of the left stick
        float leftStickH = Input.GetAxis("LeftStickH");
        float leftStickV = Input.GetAxis("LeftStickV");

        //getting the input axis for both the horezontal and vertical of the right stick
        float rightStickH = Input.GetAxis("RightStickH");
        float rightStickV = Input.GetAxis("RightStickV");

        //getting the input axis for both the horezontal and vertical of the Dpad
        float dPadH = Input.GetAxis("DpadH");
        float dPadV = Input.GetAxis("DpadV");

        intForAnim = 0;

        if (rightStickH > 0.2 || rightStickV > 0.2 || rightStickH < -0.2 || rightStickV < -0.2)
        {
            Turning(rightStickH, -rightStickV);
        }
        
        //adding some limitations for the controllers sinsitivity
        if (leftStickH > 0.2 || leftStickV > 0.2 || leftStickH < -0.2 || leftStickV < -0.2)
        {
            Move(leftStickH, -leftStickV);
            animValueH = leftStickH;
            animValueV = leftStickV;
            intForAnim = 1;
        }

        if (dPadH > 0 || dPadV > 0 || dPadH < 0 || dPadV < 0)
        {
            Move(dPadH, dPadV);
            animValueH = dPadH;
            animValueV = dPadV;
            intForAnim = 2;
        }

        Animating(animValueH, -animValueV, intForAnim);
	}

    void Move(float h, float v)
    {
        //setting the value of "move" to the horezontal & vertical axis
        movement.Set(h, 0f, v);
        //making sure that the player moves at the same speed even if the player uses h&v, then multiplaying it by speed, then multiplaying it by time.deltatime to make sure that it happens with normal time
        movement = movement.normalized * MovementSpeed * Time.deltaTime;
        //adding the movement to the rigidbody
        transform.position += movement;
    }

    void Turning(float h, float v)
    {
        Vector3 inputDirection = Vector3.zero;
        inputDirection.x = h;
        inputDirection.z = v;
        inputDirection += transform.position;

        this.transform.LookAt(inputDirection);
    }

    void Animating(float h, float v, int fromDpad)
    {
        bool walking;
        if (fromDpad == 1)
        {
            walking = h > 0.2 || v > 0.2 || h < -0.2 || v < -0.2;
        }
        else if (fromDpad == 2)
        {
            walking = h > 0 || v > 0 || h < 0 || v < 0;
        }
        else
        {
            walking = false;
        }
        anim.SetBool("IsWalking", walking);
    }
}
