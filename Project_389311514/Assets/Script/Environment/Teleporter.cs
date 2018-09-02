using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Teleporter[] OtherTeleporters;//change to list
    public GameObject TeleportingLocation;//refrence

    GameObject player;
    bool startCount;
    float timer;
    bool canUse;//use for limitations

	void Awake ()
    {
        timer = 5;

        OtherTeleporters = FindObjectsOfType<Teleporter>();

        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update ()
    {
        if (startCount == true)
        {
            timer -= Time.deltaTime;
            print(timer);

            if (timer <= 0)
            {
                int selectedTeleporterIndicator = Random.Range(0, OtherTeleporters.Length);
                Teleporter DestTele = OtherTeleporters[selectedTeleporterIndicator];
                Vector3 teleLocation = DestTele.TeleportingLocation.transform.position;
                player.transform.position = teleLocation;
                print("teleported");
                //add sound
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            startCount = true;
            //add sound
            //add patrticles
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            startCount = false;
            timer = 5f;
        }
    }
}
