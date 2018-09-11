using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentRound;
    public string playerName;
    public bool gameWon;
    public bool betweenRounds;
    public int gameMode;

    public AudioClip midGameAudio;
    public AudioClip betweenRoundsAudio;

    public Info iS;

    int leaderboardScene;


    void Awake ()
    {
        leaderboardScene = 3;
        currentRound = 1;
        Invoke("GetInfo", 5);
    }
	
    public void WinGame()
    {
        gameWon = true;
        //add win text
        Invoke("EndGame", 5);
    }

    public void LostGame()
    {
        gameWon = false;
        //add lose text
        Invoke("EndGame", 5);
    }

    public void ChangeMusic()
    {
        if (betweenRounds == true)
        {
            AudioSource bG = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
            bG.clip = betweenRoundsAudio;
            bG.Play();
        }
        else
        {
            AudioSource bG = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
            bG.clip = midGameAudio;
            bG.Play();
        }
    }

    void EndGame()
    {
        setInfo();
        //give [win or lose - survived (round.time from current fround) - times got hit - kills with special ability - used camo hat - played in slow mo - played in speed - maps open]
        SceneManager.LoadScene(3);
    }

    void GetInfo()
    {
        iS = FindObjectOfType<Info>();
        playerName = iS.playerName;
        gameMode = iS.gameMode;
    }

    void setInfo()
    {
        iS.roundsSurvived = currentRound - 1;
        iS.won = gameWon;

        Screen screenScript = FindObjectOfType<Screen>();
        float timeSurvived;
        if (screenScript.roundTime <= 0)
        {
            timeSurvived = 0;
        }
        else
        {
            timeSurvived = 90 - screenScript.roundTime;
        }
        iS.secondsInLastRound = timeSurvived;
    }
}
