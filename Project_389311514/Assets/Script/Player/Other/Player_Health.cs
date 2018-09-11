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

                                              
    Animator anim;                            
    AudioSource playerAudio;                  
    Player_Controller_Movement playerMovement;
    Player_Attack playerAttack;               
    bool isDead;
    float nuggetPoints;
    GameManager gMs;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<Player_Controller_Movement>();
        playerAttack = GetComponentInChildren<Player_Attack>();
        gMs = FindObjectOfType<GameManager>();
        
        currentHealth = startingHealth;
    }


    void Update()
    {
    }


    public void TakeDamage(float amount)
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

        gMs.LostGame();
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Nugget") && currentHealth > 100)
        {
            currentHealth += nuggetPoints;
            Mathf.Clamp(currentHealth, 0, 100);
        }
    }
}
