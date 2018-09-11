using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Teleporter[] OtherTeleporters;//change to list
    public GameObject TeleportingLocation;//refrence
    public Material[] teleporterMaterials;
    public AudioClip[] teleClips;

    GameObject player;
    bool startCount;
    float timer;
    bool canUse;
    float coolDownTime;
    int usesLeft;
    Renderer renderer;
    AudioSource teleAudio;

    void Awake ()
    {
        timer = 5;

        usesLeft = 3;

        renderer = GetComponent<Renderer>();

        OtherTeleporters = FindObjectsOfType<Teleporter>();

        teleAudio = GetComponent<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player");

        canUse = true;

        renderer.material = teleporterMaterials[usesLeft];
    }
	
	void Update ()
    {
        if (startCount == true)
        {
            float oldTimer = timer;
            timer -= Time.deltaTime;
            if (oldTimer < timer)
            {
                teleAudio.clip = teleClips[2];
                teleAudio.Play();
            }

            if (timer <= 0)
            {
                int selectedTeleporterIndicator = Random.Range(0, OtherTeleporters.Length);
                Teleporter DestTele = OtherTeleporters[selectedTeleporterIndicator];
                Vector3 teleLocation = DestTele.TeleportingLocation.transform.position;
                canUse = false;
                startCount = false;
                player.transform.position = teleLocation;
                coolDownTime = 10;//
                renderer.material = teleporterMaterials[0];
                teleAudio.clip = teleClips[3];
                teleAudio.Play();
                usesLeft--;
            }
        }

        if (canUse == false && usesLeft > 0)
        {
            coolDownTime -= Time.deltaTime;

            if (coolDownTime <= 0)
            {
                teleAudio.clip = teleClips[4];
                teleAudio.Play();
                canUse = true;
                renderer.material = teleporterMaterials[usesLeft];
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canUse == true)
        {
            startCount = true;
            timer = 5f;
            teleAudio.clip = teleClips[1];
            teleAudio.Play();
            //add patrticles
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canUse == true)
        {
            startCount = false;
            timer = 5f;
            teleAudio.clip = teleClips[1];
            teleAudio.Play();
        }
    }
}
