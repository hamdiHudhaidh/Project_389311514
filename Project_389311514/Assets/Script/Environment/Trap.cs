using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
   // public Animator anim;
    public AudioSource audio;
    public AudioClip[] trapAudio;

    public GameObject trapLaser;

    bool trapOn;
    float trapTime;
    //Collider col;

    void Awake ()
    {
        //anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        //col = trapLaser.GetComponent<Collider>();
        trapLaser.SetActive(false);
    }

    void FixedUpdate()
    {
        if (trapOn == true)
        {
            trapTime -= Time.deltaTime;

            if (trapTime <= 0)
            {
                TrapDone();
            }
        }
    }

    public void useTrap()
    {
        //anim.SetBool("Trap_On", true);
        audio.clip = trapAudio[0];
        audio.Play();
        Invoke("TrapOn", 1);
        //col.enabled = !col.enabled;
    }

    void TrapOn()
    {
        trapLaser.SetActive(true);
        audio.loop = true;
        audio.clip = trapAudio[1];
        audio.Play();
        trapTime = 30f;
        trapOn = true;
    }

    public void TrapDone()
    {
        //anim.SetBool("Trap_On", false);
        audio.Stop();
        trapLaser.SetActive(false);
        trapOn = false;
        audio.loop = false;
        //col.enabled = !col.enabled;
    }
}

