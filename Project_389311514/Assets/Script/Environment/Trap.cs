using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject[] trapPartsA;
    public GameObject[] trapPartsB;

    bool trapWorking;
    bool partsA;
    bool usingTimer;
    float timeLeft;
    float speed;

    Vector3 downPosition;
    Vector3 upPosition;


    void Start ()
    {

    }
	
	void FixedUpdate ()
    {
        if (trapWorking == true)
        {
            if (partsA == true)
            {
                for (int i = 0; i < trapPartsA.Length; i++)
                {
                    downPosition = trapPartsA[i].transform.position;
                    upPosition = downPosition + new Vector3(0, 4, 0);

                    trapPartsA[i].transform.position = Vector3.Lerp(downPosition, upPosition, Mathf.PingPong(Time.time * speed, 1.0f));


                }
            }
            else if (partsA == false)
            {

            }
        }

        //after time change trapWorking = false
        //get parts down
        if (usingTimer == true)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {

                partsA = true;
                trapWorking = false;
            }
        }
    }

    public void useTrap()
    {
        trapWorking = true;
    }
}

