using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    public GameObject nugget;
    public GameObject gate;

    bool gateUp;
    bool gateShouldMove;
    bool shouldKill;

    Vector3 downPosition;
    Vector3 upPosition;
    float speed;

	void Awake ()
    {
        downPosition = gate.transform.position;
        upPosition = downPosition + new Vector3(0, 4, 0);
    }
	
	void FixedUpdate ()
    {
        if (gateShouldMove == true)
        {
            if (gateUp == true)
            {
                gate.transform.position = Vector3.Lerp(downPosition, upPosition, Mathf.PingPong(Time.time * speed, 1.0f));

                if (gate.transform.position == upPosition)
                {
                    gateShouldMove = false;
                    //do fire effect
                    shouldKill = true;
                    Kill();
                    Invoke("Kill", 5);
                }
            }
            else if (gateUp == false)
            {
                gate.transform.position = Vector3.Lerp(upPosition, downPosition, Mathf.PingPong(Time.time * speed, 1.0f));

                if (gate.transform.position == downPosition)
                {
                    gateShouldMove = false;
                    gateUp = true;
                }
            }
        }
	}

    public void UseOven()
    {
        gateShouldMove = true;
        gateUp = true;
    }

    void Kill()
    {
        gateShouldMove = true;
        gateUp = true;
    }

    /*void OnTriggerEnter(Collider[] other)
    {
        if (shouldKill == true)
        {
            //GameObject[] chickensInOven = other.
            for (int i = 0; i < other.Length; i++)
            {
                Instantiate(nugget, other[i].gameObject.transform.position, other[i].gameObject.transform.rotation);

                Destroy(other[i]);
                if (i == other.Length)
                {
                    shouldKill = false;
                }
            }
        }
    }*/
}
