using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public GameManager gMs;
    public AudioClip spawnClip;

    GameObject[] enemies;
    GameObject[] spawnLocations;
    public AudioSource audio;

    float repeatRate;
    float maximumEnemies;
    bool spawnOn;
    float tillNextSpawn;


	void Start ()
    {
        repeatRate = 1;
        maximumEnemies = 20;
        tillNextSpawn = 5;
        repeatRate = gMs.currentRound;
        gMs.GetComponent<GameManager>();
        audio.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (gMs.betweenRounds == false)
        {
            if (spawnOn == true)
            {
                for (int i = 1; i <= repeatRate; i++)
                {
                    spawnOn = false;
                    tillNextSpawn = 5;
                    maximumEnemies += gMs.currentRound;
                    Invoke("Spawn", i);
                }
            }

            if (spawnOn == false)
            {
                tillNextSpawn -= Time.deltaTime;

                if (tillNextSpawn <= 0)
                {
                    spawnOn = true;
                }
            }
        }
    }

    void Spawn()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        spawnLocations = GameObject.FindGameObjectsWithTag("Spawn_Point");

        repeatRate = gMs.currentRound;

        if (enemies.Length < maximumEnemies)
        {
            Transform currentSpawnPoint = spawnLocations[Random.Range(0, spawnLocations.Length)].transform;

            Instantiate(enemy, currentSpawnPoint.position, currentSpawnPoint.rotation);

            audio.clip = spawnClip;
            audio.Play();
        }
    }
}