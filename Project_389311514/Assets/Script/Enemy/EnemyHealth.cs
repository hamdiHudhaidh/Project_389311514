﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float initialEnemyHealth = 100f;
    public float currentEnemyHealth;
    public float deathPoints = 20f;
    public Image healthBar;
    public GameObject egg;

	void Awake ()
    {
        currentEnemyHealth = initialEnemyHealth;
        deathPoints = 5;
	}
	
	void Update ()
    {
        if (currentEnemyHealth <= 0)
        {
            int chancesOfDropping = Random.Range(0, 100);
            if (chancesOfDropping <= 100)
            {
                Instantiate(egg, transform.position, transform.rotation);
            }
            //Player_Attack pAs = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Attack>();
            //pAs.specialAbilityBar += deathPoints;
            Destroy(this.gameObject);
        }
	}
}
