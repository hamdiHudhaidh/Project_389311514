using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public GameObject startText;
    public GameObject TwelveRoundsButton;
    public GameObject JustSurviveButton;
    public InputField playerName;
    public GameObject playButton;

    int LoadScene;
    int thisScene;
    Info iS;
    bool hasName;
    bool canPlaySurvive;
    float secretTimer;

    void Awake ()
    {
        iS = FindObjectOfType<Info>();
        LoadScene = 1;
        thisScene = 0;
        secretTimer = 8;
        startText.SetActive(true);
        TwelveRoundsButton.SetActive(false);
        JustSurviveButton.SetActive(false);
        playButton.SetActive(false);
        canPlaySurvive = false;
    }
	
	void Update ()
    {
        if (playerName.text.Length >= 3)
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }

        if (Input.GetButton("Start") && Input.GetButton("Select"))
        {
            secretTimer -= Time.deltaTime;
            print("doing it");

            if (secretTimer < 0)
            {
                canPlaySurvive = true;
                print("did it");
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(thisScene);
        }
	}

    public void play()
    {
        iS.playerName = playerName.text;
        startText.SetActive(false);
        TwelveRoundsButton.SetActive(true);
        if (canPlaySurvive)
        {
            JustSurviveButton.SetActive(true);
        }
    }

    public void TwelveRounds()
    {
        iS.gameMode = 1;
        SceneManager.LoadScene(LoadScene);
    }

    public void JustSurvive()
    {
        iS.gameMode = 2;
        SceneManager.LoadScene(LoadScene);
    }
}
