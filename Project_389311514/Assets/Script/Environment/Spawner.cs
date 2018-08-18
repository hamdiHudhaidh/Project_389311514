using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;

    //
    public GameObject spawner;

    GameObject[] enemies;
    GameObject[] spawners;
    Transform[] spawnLocations;

	void Start ()
    {
        //SettingSpawners();

        //
        //enemy.transform.position = spawner.transform.position;
        Instantiate(enemy, transform.position, transform.rotation);
        print("enemy" + enemy.transform.position + "spawner" + spawner.transform.position);
    }
	
	void Update ()
    {
		
	}

    void SettingSpawners()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            if (spawners[i].activeInHierarchy)
            {

            }
        }
    }
}
