using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    int gameScene;
    bool canCont;
    public Text contText;


    void Start ()
    {
        gameScene = 2;
        Invoke("CanPlay", 10);
	}

    void Update()
    {
        if (Input.GetButtonDown("Start") && canCont == true)
        {
            ChangeScene();
        }
    }

    void CanPlay()
    {
        canCont = true;
        contText.text = "Press Start To Continue";
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(gameScene);
    }
}
