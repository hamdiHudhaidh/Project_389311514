using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{ 
    public float startingHealth = 100;                           
    public float currentHealth;
    public Image healthBar;
    public AudioClip deathAudio;
    public AudioClip hurtAudio;


    Animator anim;                                              // Reference to the Animator component.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    Player_Controller_Movement playerMovement;                  // Reference to the player's movement.
    Player_Attack playerAttack;                                 // Reference to the PlayerShooting script.
    bool isDead;                                                // Whether the player is dead.

    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        //playerMovement = GetComponent<Player_Controller_Movement>();
        //playerAttack = GetComponentInChildren<Player_Attack>();
        
        // Set the initial health of the player.
        currentHealth = startingHealth;
    }


    void Update()
    {
        /*if (currentHealth != 100 && !isDead)
        {
            Invoke("ReturnHealth", 2);
        }*/
        //print(currentHealth);
        //change the movement animation depending on the health
    }


    public void TakeDamage(int amount)
    {
        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthBar.fillAmount = currentHealth / startingHealth;

        // Play the hurt sound effect.
        playerAudio.clip = hurtAudio;
        playerAudio.Play();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Tell the animator that the player is dead.
        anim.SetTrigger("Die");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        playerAudio.clip = deathAudio;
        playerAudio.Play();

        // Turn off the movement and shooting scripts.
        playerMovement.enabled = false;
        playerAttack.enabled = false;
    }

    void ReturnHealth()
    {
        currentHealth += 15;
    }
}
