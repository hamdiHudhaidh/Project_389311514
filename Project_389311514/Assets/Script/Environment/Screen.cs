using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screen : MonoBehaviour
{
    //general
    public bool gamePaused;

    [Header("timer canvas")]
    public GameObject timerCanvas;
    public Text roundText;
    public Text timerText;

    [Header("win canvas")]
    public GameObject winCanvas;
    public Text winPlayerName;

    [Header("loose canvas")]
    public GameObject looseCanvas;
    public Text loosePlayerName;

    [Header("pause menu")]
    public GameObject pauseMenu_Buttons;
    public GameObject pauseMenu_ResumeTime;
    public Text pauseMenu_ResumeTime_Txt;
    public GameObject pauseGameCanvas;

    [Header("pause menu controls")]
    public GameObject controls;


    float roundTime;
    float betweenRoundTime;
    GameManager gMs;
    bool resumingGame;
    float resumingGameTime;

    float first;

    void Awake ()
    {
        GameObject gameManagerObject = GameObject.Find("Game_Manager");
        gMs = gameManagerObject.GetComponent<GameManager>();

        gMs.betweenRounds = true;
        roundTime = 10f;//
        betweenRoundTime = 10f;//
        resumingGameTime = 5f;

        roundText.text = "Round 1 Start's in";

        timerCanvas.SetActive(true);
    }
	
	void Update ()
    {
        if (gamePaused == false)
        {
            if (gMs.betweenRounds == false)
            {
                roundTime -= Time.deltaTime;
                timerText.text = roundTime.ToString("##");

                if (roundTime <= 0)
                {
                    gMs.currentRound++;
                    gMs.betweenRounds = true;
                    roundText.text = "Round " + gMs.currentRound.ToString() + " Start's in";
                    roundTime = 10f;//
                    if (gMs.currentRound == 13)
                    {
                        timerCanvas.SetActive(false);
                        winCanvas.SetActive(true);
                        winPlayerName.text = gMs.playerName;
                        gMs.WinGame();
                    }
                }
            }
            else if (gMs.betweenRounds == true)
            {
                betweenRoundTime -= Time.deltaTime;
                timerText.text = betweenRoundTime.ToString("##");

                if (betweenRoundTime < 0)
                {
                    roundText.text = "Round " + gMs.currentRound.ToString();
                    gMs.betweenRounds = false;
                    betweenRoundTime = 10f;//
                }
            }
        }

        if (resumingGame == true)
        {
            if (first < 1)//CHANGE TO fix bug
            {
                first = Time.realtimeSinceStartup + 5;
            }
            resumingGameTime = first - Time.realtimeSinceStartup;
            pauseMenu_ResumeTime_Txt.text = resumingGameTime.ToString("##");
            print(Time.realtimeSinceStartup + "    " + first);
            if (Time.realtimeSinceStartup > first)
            {
                timerCanvas.SetActive(true);
                pauseGameCanvas.SetActive(false);
                gamePaused = false;
                resumingGame = false;
                Time.timeScale = 1;//fix to match slow motion effect
                betweenRoundTime = 5f;
            }
        }
    }

    public void PauseGame()
    {
        timerCanvas.SetActive(false);
        pauseGameCanvas.SetActive(true);
        pauseMenu_Buttons.SetActive(true);
        pauseMenu_ResumeTime.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        resumingGame = true;
        pauseMenu_Buttons.SetActive(false);
        pauseMenu_ResumeTime.SetActive(true);
    }

    public void Controls()
    {
        pauseGameCanvas.SetActive(false);
        controls.SetActive(true);
    }

    public void Controls_Back()
    {
        controls.SetActive(false);
        pauseGameCanvas.SetActive(true);
        pauseMenu_Buttons.SetActive(true);
        pauseMenu_ResumeTime.SetActive(false);
    }
}
