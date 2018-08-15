using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    AudioSource explosionAudio;

	void Start ()
    {
        explosionAudio = GetComponent<AudioSource>();

        explosionAudio.Play();

        Destroy(this.gameObject, 2);
    }
}
