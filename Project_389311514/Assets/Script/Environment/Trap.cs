using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Animator anim;
    public AudioSource audio;
    public AudioClip trapAudio;

    bool colliderOn;

    void Awake ()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    public void useTrap()
    {
        anim.SetBool("Trap_On", true);
        audio.clip = trapAudio;
        audio.Play();
        colliderOn = true;
    }

    public void AnimationDone()
    {
        anim.SetBool("Trap_On", false);
        audio.Stop();
        colliderOn = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (colliderOn == true && collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}

