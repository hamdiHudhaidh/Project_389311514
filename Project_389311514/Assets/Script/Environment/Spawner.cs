using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public GameManager gMs;

    GameObject[] enemies;
    GameObject[] spawnLocations;

    float repeatRate;
    float maximumEnemies;
    float amountOfEnemies;

	void Start ()
    {
        repeatRate = 8;
        maximumEnemies = 20;
        gMs.GetComponent<GameManager>();

        InvokeRepeating("CheckSpawn", 0, repeatRate);//change to a timer in update that checks if in round
    }

    void CheckSpawn()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //print(enemies.Length);

        spawnLocations = GameObject.FindGameObjectsWithTag("Spawn_Point");

        amountOfEnemies = gMs.currentRound;

        if (enemies.Length < maximumEnemies)
        {
            for (int i = 0; i < amountOfEnemies; i++)
            {
                Transform currentSpawnPoint = spawnLocations[Random.Range(0, spawnLocations.Length)].transform;

                Instantiate(enemy, currentSpawnPoint.position, currentSpawnPoint.rotation);
            }
        }
    }
}