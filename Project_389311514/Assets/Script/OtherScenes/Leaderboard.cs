using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    public Text gameState;
    public Text playerName;
    public Text roundsSurvived;
    public Text timeSurvivedInLastRound;

    //previous player
    public Text gameState_Ps;
    public Text playerName_Ps;
    public Text roundsSurvived_Ps;
    public Text timeSurvivedInLastRound_Ps;

    Info iS;
    int startScene;

	void Start ()
    {
        iS = FindObjectOfType<Info>();
        startScene = 0;

        if (iS.won == true)
        {
            gameState.text = "You Survived 12 Rounds!!";
        }
        else
        {
            gameState.text = "You Lost...";
        }

        playerName.text = iS.playerName;

        roundsSurvived.text = "survived " + iS.roundsSurvived + " Rounds";

        timeSurvivedInLastRound.text = "And " + iS.secondsInLastRound + " Seconds";//

        iS.Load();

        if (iS.won_Ps == false)
        {
            gameState_Ps.text = "Previous Session: Lost";
        }
        else
        {
            gameState_Ps.text = "Previous Session: Won";
        }

        playerName_Ps.text = iS.playerName_Ps;

        roundsSurvived_Ps.text = "survived " + iS.roundsSurvived_Ps + " Rounds";

        timeSurvivedInLastRound_Ps.text = "And " + iS.secondsInLastRound_Ps + " Seconds";

        iS.Save();
    }

    public void Exit()
    {
        Destroy(iS.gameObject);
        SceneManager.LoadScene(startScene);
    }
}
