using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public GameObject player;

    public Rigidbody rB;

    public Animator anim;

    public AudioSource chickenAudio;
    public AudioClip attackAudio;

    Vector3 target;
    bool needPathFinding;
    float speed;
    float attackDistance;
    float attackDamage;
    bool canAttack;
    bool attacking;


    void Awake ()
    {
        rB.GetComponent<Rigidbody>();
        anim.GetComponent<Animator>();
        chickenAudio.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");

        //target = player.transform.position;

        speed = 3f;

        attackDistance = 2f;

        attackDamage = 30f;

        canAttack = true;
    }
	
	void FixedUpdate ()
    {
        //FindPath();
        //IfNeedPathFinding();
        ChaseAndAttack();
    }

    void IfNeedPathFinding()
    {
        //check if the player is on the same map area
    }

    void FindPath()
    {
        //set target
    }

    void ChaseAndAttack()//add collision avoidancce
    {
        if (Vector3.Distance(target, transform.position) <= attackDistance && canAttack == true)
        {
            //change canAttack to false and change it backafter anim
            canAttack = false;
            attacking = true;
            //play attack anim
            anim.SetBool("Attack", true);
            //play sound
            chickenAudio.clip = attackAudio;
            chickenAudio.Play();
            //deduct health
            Player_Health playerHealth = player.GetComponent<Player_Health>();//change to check if at point of attack the playert is still close
            playerHealth.currentHealth -= attackDamage;
        }
        else if (attacking == false)
        {
            target = player.transform.position;
            transform.LookAt(target);
            rB.AddForce(transform.forward * speed * 10f);//for friction
            rB.velocity = Vector3.ClampMagnitude(rB.velocity, speed);
        }
    }

    //called after animation
    void EnableAttack()
    {
        canAttack = true;
        attacking = false;
        anim.SetBool("Attack", false);
    }
}
