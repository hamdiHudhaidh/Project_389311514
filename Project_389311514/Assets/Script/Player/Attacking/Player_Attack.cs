using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Attack : MonoBehaviour
{
    public Animator gunAnim;
    public AudioClip attackAudio;
    public AudioClip specialAttackAudio;
    public float specialAbilityBar;
    public GameObject specialAttackBullet;
    public GameObject bulletLocation;
    public Player_PickUp pUs;
    public Image sAI;

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
        pUs = GetComponent<Player_PickUp>();

        gunAnim = GetComponent<Animator>();

        gunAudio = GetComponent<AudioSource>();

        canAttack = true;

        specialAbilityBar = 0;

        sAI.color = Color.blue;
    }
	

	void FixedUpdate ()
    {
        if (specialAbilityBar == 100)
        {
            sAI.color = Color.yellow;
        }

        if ((Input.GetButtonDown("R2") || Input.GetButtonDown("Fire1")) && canAttack == true && specialAbilityOn == false)
        {
            canAttack = false;
            gunAnim.SetBool("Attack", true);
            gunAudio.clip = attackAudio;
            gunAudio.Play();
        }
        else if ((Input.GetButtonDown("R2") || Input.GetButtonDown("Fire1")) && canAttack == true && specialAbilityOn == true)
        {
            //canAttack = false;
            Instantiate(specialAttackBullet, bulletLocation.transform.position, bulletLocation.transform.rotation);
            gunAudio.clip = specialAttackAudio;
            gunAudio.Play();
        }

        if ((Input.GetButtonDown("R1") && Input.GetButtonDown("L1") || Input.GetButtonDown("Fire2")) && specialAbilityBar == 100)
        {
            EnableSpecialAbility();
        }
	}

    public void AllowAttack()
    {
            canAttack = true;
            gunAnim.SetBool("Attack", false);
    }

    void EnableSpecialAbility()
    {
        specialAbilityOn = true;
        InvokeRepeating("DecreaseSpecialAbilityBar", 1, 1);
    }

    void DisableSpecialAbility()
    {
        specialAbilityOn = false;
        sAI.color = Color.blue;
    }

    //lower special ability bar at a rate of 5 points a second = 20 seconds ability
    void DecreaseSpecialAbilityBar()
    {
        specialAbilityBar -= decreaseAmount;
        Mathf.Clamp(specialAbilityBar, 0, 100);
        sAI.fillAmount = specialAbilityBar / 100;

        if (specialAbilityBar <= 0)
        {
            DisableSpecialAbility();
        }
    }

    void Attack()
    {
        if (Physics.Raycast(bulletLocation.transform.position, bulletLocation.transform.forward, out hit, normalBulletRange))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                enemyHealth.currentEnemyHealth -= normalBulletDamage;
                enemyHealth.healthBar.fillAmount = enemyHealth.currentEnemyHealth / enemyHealth.initialEnemyHealth;
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
