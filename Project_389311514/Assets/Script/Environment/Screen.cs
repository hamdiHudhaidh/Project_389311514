using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Button pMcButton;
    bool instOn;


    public float roundTime;
    float initialRoundTime;
    float betweenRoundTime;
    float initialBetweenRoundTime;
    GameManager gMs;
    bool resumingGame;
    float resumingGameTime;
    float initialResumingGameTime;

    float first;
    PM_Controls pauseMenuconrols;

    MemberConfig conf;

    int startScene;

    Color green = Color.green;

    void Awake ()
    {
        GameObject gameManagerObject = GameObject.Find("Game_Manager");
        gMs = gameManagerObject.GetComponent<GameManager>();

        conf = FindObjectOfType<MemberConfig>();

        gMs.betweenRounds = true;
        roundTime = 90;
        initialRoundTime = 90;
        betweenRoundTime = 10;//
        initialBetweenRoundTime = 30;
        resumingGameTime = 5f;

        startScene = 0;

        roundText.text = "Round 1 Start's in";

        timerCanvas.SetActive(true);

        pauseMenuconrols = GetComponent<PM_Controls>();
    }
	
	void Update ()
    {
        if (gamePaused == false)
        {
            if (gMs.betweenRounds == false)
            {
                roundTime -= Time.deltaTime;
                timerText.text = roundTime.ToString("##");

                if (roundTime <= 0 && conf.members.Length <= 0)
                {
                    roundTime = 0;
                    if (conf.members.Length <= 0)
                    {
                        gMs.currentRound++;
                        gMs.betweenRounds = true;
                        roundText.text = "Round " + gMs.currentRound.ToString() + " Start's in";
                        roundTime = initialRoundTime;
                        gMs.ChangeMusic();
                        if (gMs.currentRound == 13 && gMs.gameMode == 1)
                        {
                            timerCanvas.SetActive(false);
                            winCanvas.SetActive(true);
                            winPlayerName.text = gMs.playerName;
                            gMs.WinGame();
                        }
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
                    betweenRoundTime = initialBetweenRoundTime;
                    gMs.ChangeMusic();
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
            if (Time.realtimeSinceStartup > first)
            {
                timerCanvas.SetActive(true);
                pauseGameCanvas.SetActive(false);
                gamePaused = false;
                resumingGame = false;
                Time.timeScale = 1;//fix to match slow motion effect
                betweenRoundTime = initialBetweenRoundTime;
            }
        }

        if (instOn == true && Input.GetButtonUp("X"))
        {
            pMcButton.onClick.Invoke();
        }
    }

    public void PauseGame()
    {
        timerCanvas.SetActive(false);
        pauseGameCanvas.SetActive(true);
        pauseMenu_Buttons.SetActive(true);
        pauseMenu_ResumeTime.SetActive(false);
        pauseMenuconrols.enabled = !pauseMenuconrols.enabled;
        Time.timeScale = 0;
        first = 0;
    }

    public void ResumeGame()
    {
        resumingGame = true;
        pauseMenu_Buttons.SetActive(false);
        pauseMenu_ResumeTime.SetActive(true);
        pauseMenuconrols.enabled = !pauseMenuconrols.enabled;
    }

    public void Controls()//button
    {
        pauseGameCanvas.SetActive(false);
        controls.SetActive(true);
        Image img = pMcButton.GetComponent<Image>();
        img.color = green;
        instOn = true;
        pauseMenuconrols.enabled = !pauseMenuconrols.enabled;
    }

    public void Controls_Back()//button
    {
        controls.SetActive(false);
        pauseGameCanvas.SetActive(true);
        pauseMenu_Buttons.SetActive(true);
        pauseMenu_ResumeTime.SetActive(false);
        instOn = false;
        pauseMenuconrols.enabled = !pauseMenuconrols.enabled;
    }

    public void Quit()//button
    {
        print("quiting");
        Info iS = FindObjectOfType<Info>();
        Destroy(iS.gameObject);
        SceneManager.LoadScene(startScene);
    }
}
