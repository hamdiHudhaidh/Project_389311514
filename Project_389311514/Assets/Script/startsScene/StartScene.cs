using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    int mainMenuScene;

	void Awake ()
    {
        mainMenuScene = 1;
    }
	
	void Update ()
    {
        if (Input.GetButtonDown("Start"))
        {
            SceneManager.LoadScene(mainMenuScene);
        }

        //add words fade
	}
}
