using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screen : MonoBehaviour
{
    public bool betweenRounds;
    public bool gamePaused;
    public Text roundText;
    public Text timerText;

    float roundTime;
    float betweenRoundTime;
    GameManager gMs;

    void Awake ()
    {
        GameObject gameManagerObject = GameObject.Find("Game_Manager");
        gMs = gameManagerObject.GetComponent<GameManager>();

        betweenRounds = true;
        roundTime = 10f;
        betweenRoundTime = 10f;

        roundText.text = "Round 1 Start's in";
    }
	
	void Update ()
    {
        if (gamePaused == false)
        {
            if (betweenRounds == false)
            {
                roundTime -= Time.deltaTime;
                timerText.text = roundTime.ToString("##");

                if (roundTime <= 0)
                {
                    gMs.currentRound++;
                    betweenRounds = true;
                    roundText.text = "Round " + gMs.currentRound.ToString() + " Start's in";
                    roundTime = 10f;
                }
            }
            else if (betweenRounds == true)
            {
                betweenRoundTime -= Time.deltaTime;
                timerText.text = betweenRoundTime.ToString("##");

                if (betweenRoundTime < 0)
                {
                    roundText.text = "Round " + gMs.currentRound.ToString();
                    betweenRounds = false;
                    betweenRoundTime = 10f;
                }
            }
        }
        else if (gamePaused == true)
        {
            // add pause screen
        }
    }
}
