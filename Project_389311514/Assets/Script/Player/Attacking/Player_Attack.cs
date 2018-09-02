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
    public Player_PickUp pUs;

    AudioSource gunAudio;
    bool canAttack;
    bool specialAbilityOn;
    float decreaseAmount = 5f;
    RaycastHit hit;
    float normalBulletRange = 3f;
    float normalBulletDamage = 33.34f;
    float specialBulletDamage = 150f;


    void Awake ()
    {
        pUs = transform.parent.GetComponent<Player_PickUp>();

        gunAnim = GetComponent<Animator>();

        gunAudio = GetComponent<AudioSource>();

        canAttack = true;

        specialAbilityBar = 0;

        //
        //specialAbilityOn = true;
    }
	

	void FixedUpdate ()
    {
        if ((Input.GetButtonDown("R2") || Input.GetButtonDown("Fire1")) && canAttack == true && specialAbilityOn == false)
        {
            //canAttack = false;
            //gunAnim.SetBool("Attack", true);
            Attack();
            gunAudio.clip = attackAudio;
            gunAudio.Play();
        }
        else if ((Input.GetButtonDown("R2") || Input.GetButtonDown("Fire1")) && canAttack == true && specialAbilityOn == true)
        {
            //canAttack = false;
            gunAnim.SetBool("Attack", true);
            Instantiate(specialAttackBullet, bulletLocation.transform.position, bulletLocation.transform.rotation);
            //glove.SetActive(false);
            gunAudio.clip = specialAttackAudio;
            gunAudio.Play();
        }

        if ((Input.GetButtonDown("R1") && Input.GetButtonDown("L1") || Input.GetButtonDown("Fire2")) && specialAbilityBar == 100)
        {
            EnableSpecialAbility();
        }

        //change its location
        Mathf.Clamp(specialAbilityBar, 0, 100);
	}

    //called after an animation
    public void AllowAttack()
    {
        if (specialAbilityOn == true)// work on this more after animation clear
        {
            gunAnim.SetBool("Attack", false);
            gunAnim.SetBool("Reload", true);
        }
        else
        {
            canAttack = true;
            gunAnim.SetBool("Attack", false);
        }
    }

    void EnableSpecialAbility()
    {
        specialAbilityOn = true;
        InvokeRepeating("DecreaseSpecialAbilityBar", 1, 1);
    }

    //lower special ability bar at a rate of 5 points a second = 20 seconds ability
    void DecreaseSpecialAbilityBar()
    {
        specialAbilityBar -= decreaseAmount;
    }

    void Attack()
    {
        if (Physics.Raycast(bulletLocation.transform.position, bulletLocation.transform.forward, out hit, normalBulletRange))
        {
            if (hit.collider.name == "Enemy")
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                enemyHealth.currentEnemyHealth -= normalBulletDamage;
                enemyHealth.healthBar.fillAmount = enemyHealth.currentEnemyHealth / enemyHealth.initialEnemyHealth;
                //change location to after enemy is dead
                //specialAbilityBar += enemyHealth.deathPoints;
            }
            else if (hit.collider.name == "Pause_Button")
            {
                Screen screenScript = hit.collider.transform.parent.GetComponent<Screen>();
                screenScript.PauseGame();
            }
            else if (hit.collider.name == "Oven_Operator" && pUs.hasGoldCoin == true)
            {
                Oven ovenScript = hit.collider.transform.parent.GetComponent<Oven>();
                ovenScript.UseOven();
                pUs.GoldCoin(false);
            }
            else if (hit.collider.name == "Trap" && pUs.hasGoldCoin == true)
            {
                Trap trapScript = hit.collider.transform.parent.GetComponent<Trap>();
                trapScript.useTrap();
                pUs.GoldCoin(false);
            }
            else if (hit.collider.name == "Lock" && pUs.hasGoldCoin == true)
            {
                Map_Extention mEs = hit.collider.transform.parent.GetComponent<Map_Extention>();
                mEs.OpenArea();
                pUs.GoldCoin(false);
            }
        }
    }
}
