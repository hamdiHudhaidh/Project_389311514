using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentRound;
    public string playerName;
    public bool gameWon;
    public bool betweenRounds;


    void Awake ()
    {
        currentRound = 1;
        playerName = "Player_Name";//
    }
	
    public void WinGame()
    {
        gameWon = true;
    }

    void EndGame()
    {
        //change scene
        //give [wine or lose - survived (round.time from current fround) - times got hit - kills with special ability - used camo hat - played in slow mo - played in speed - maps open]
    }
}
