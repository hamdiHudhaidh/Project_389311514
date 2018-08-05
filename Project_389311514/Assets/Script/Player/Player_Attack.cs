using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Animator gunAnim;
    public AudioClip attackAudio;
    public AudioClip specialAttackAudio;
    public float specialAbilityBar;
    public GameObject specialAttackBullet;
    public GameObject glove;
    public GameObject bulletLocation;

    AudioSource gunAudio;
    bool canAttack;
    bool specialAbilityOn;
    float decreaseAmount = 5f;


    void Awake ()
    {
        gunAnim = GetComponent<Animator>();

        canAttack = true;

        specialAbilityBar = 0;
    }
	

	void FixedUpdate ()
    {
        if (Input.GetButtonDown("R2") && canAttack == true && specialAbilityOn == false)
        {
            canAttack = false;
            gunAnim.SetBool("Attack", true);
            //damage enemy with raycast
            gunAudio.clip = attackAudio;
            gunAudio.Play();
        }
        else if (Input.GetButtonDown("R2") && canAttack == true && specialAbilityOn == true)
        {
            canAttack = false;
            Instantiate(specialAttackBullet, bulletLocation.transform);
            glove.SetActive(false);
            //change can attack back to true after the reload animation
            gunAudio.clip = specialAttackAudio;
            gunAudio.Play();
        }

        if (Input.GetButtonDown("R1") && Input.GetButtonDown("L1") && specialAbilityBar == 100)
        {
            EnableSpecialAbility();
        }
	}

    public void AllowAttack()
    {
        canAttack = true;
        gunAnim.SetBool("Attack", false);
    }

    public void EnableSpecialAbility()
    {
        specialAbilityOn = true;
        InvokeRepeating("DecreaseSpecialAbilityBar", 1, 1);
    }

    //lower special ability bar at a rate of 5 points a second = 20 seconds ability
    void DecreaseSpecialAbilityBar()
    {
        specialAbilityBar -= decreaseAmount;
    }
}
