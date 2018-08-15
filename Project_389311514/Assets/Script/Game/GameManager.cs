using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentRound;

   

    void Awake ()
    {
        currentRound = 1;
        //end game at round 12
    }
	
	void Update ()
    {
        if (true)
        {

        }
    }

    void EndGame()
    {
        //change scene
        //give [wine or lose - survived (round.time from current fround) - times got hit - kills with special ability - used camo hat - played in slow mo - played in speed - maps open]
    }
}
