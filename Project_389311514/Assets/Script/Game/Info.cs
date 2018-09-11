using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    public string playerName;
    public int gameMode;
    public int roundsSurvived = 0;
    public float secondsInLastRound;
    public bool won;
    //public string comment;

    public string playerName_Ps;
    public int roundsSurvived_Ps;
    public float secondsInLastRound_Ps;
    public bool won_Ps;

    public bool fromLastScene;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Save()
    {
        SaveLoadData.savePlayer(this);
    }

    public void Load()
    {
        playerName_Ps = SaveLoadData.LoadPlayerName();
        won_Ps = SaveLoadData.LoadPlayerBool();
        roundsSurvived_Ps = SaveLoadData.LoadPlayerRound();
        secondsInLastRound_Ps = SaveLoadData.LoadPlayerTime();
    }
}
