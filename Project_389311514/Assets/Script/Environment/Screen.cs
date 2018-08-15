using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    bool usingTimer;
    float roundTime;
    float betweenRoundTime;
    GameManager gMs;

    void Awake ()
    {
        GameObject gameManagerObject = GameObject.Find("Game Manager");
        gMs = gameManagerObject.GetComponent<GameManager>();

        roundTime = 180f;
        betweenRoundTime = 60f;
    }
	
	void Update ()
    {
        if (usingTimer == true)
        {
            roundTime -= Time.deltaTime;

            if (roundTime <= 0)
            {
                gMs.currentRound++;
            }
        }
        else if (usingTimer == false)
        {
            betweenRoundTime -= Time.deltaTime;

            if (betweenRoundTime <= 0)
            {
                gMs.currentRound++;
            }
        }
    }
}
