using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    public GameObject nugget;
    public GameObject gate;
    public AudioClip[] ovenClips;

    bool gateUp;
    bool gateShouldMove;
    bool shouldKill;

    AudioSource ovenAudio;
    Vector3 downPosition;
    Vector3 upPosition;
    float speed;

	void Awake ()
    {
        ovenAudio = GetComponent<AudioSource>();
        downPosition = gate.transform.position;
        upPosition = downPosition + new Vector3(0, 4, 0);
        speed = 1f;
    }
	
	void FixedUpdate ()
    {
        if (gateShouldMove == true)
        {
            if (gateUp == true)
            {
                gate.transform.position = Vector3.Lerp(gate.transform.position, upPosition, Mathf.PingPong(Time.deltaTime * speed, 1.0f));

                if (gate.transform.position.y >= -0.18)
                {
                    //do fire effect
                    shouldKill = true;
                    Invoke("Kill", 5);
                    gateShouldMove = false;
                    ovenAudio.clip = ovenClips[2];
                    ovenAudio.Play();
                }
            }
            else if (gateUp == false)
            {
                gate.transform.position = Vector3.Lerp(gate.transform.position, downPosition, Mathf.PingPong(Time.deltaTime * speed, 1.0f));

                if (gate.transform.position.y <= -4.1)
                {
                    gateShouldMove = false;
                }
            }
        }
	}

    public void UseOven()
    {
        gateUp = true;
        gateShouldMove = true;
        ovenAudio.clip = ovenClips[0];
        ovenAudio.Play();
    }

    void Kill()
    {
        ovenAudio.Stop();
        gateUp = false;
        shouldKill = false;
        gateShouldMove = true;
        ovenAudio.clip = ovenClips[3];
        ovenAudio.Play();
    }

    void OnTriggerStay(Collider other)
    {
        if (shouldKill == true)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Vector3 position = other.gameObject.transform.position;
                Instantiate(nugget, other.gameObject.transform.position, other.gameObject.transform.rotation);
                Destroy(other.gameObject);
            }
        }
    }
}
