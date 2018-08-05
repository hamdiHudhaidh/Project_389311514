using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_Controller : MonoBehaviour
{
    private Vector3 position1;
    private Vector3 position2;

    public float speed;

    private bool shouldMove; 

    void Start ()
    {
        position1 = this.transform.position;

        if (this.gameObject.tag == "Oven")
        {
            position2 = position1 + new Vector3(0, 4, 0);
            shouldMove = true;
            speed = 0.5f;
        }
        else if (this.gameObject.tag == "Trap1")
        {
            position2 = position1 + new Vector3(0, 1, 0);
            shouldMove = true;
            speed = 3f;
        }
        else if (this.gameObject.tag == "Trap2")
        {
            position2 = position1 + new Vector3(0, 1, 0);
            Invoke("moveDelay", 0.5f);
            speed = 3f;
        }
	}
	

	void FixedUpdate ()
    {
        if (shouldMove == true)
        {
            transform.position = Vector3.Lerp(position1, position2, Mathf.PingPong(Time.time * speed, 1.0f));
        }
    }

    void moveDelay()
    {
        shouldMove = true;
    }
}
