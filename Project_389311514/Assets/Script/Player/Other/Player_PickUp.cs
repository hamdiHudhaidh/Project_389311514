using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_PickUp : MonoBehaviour
{
    public bool hasGoldCoin;
    public GameObject backPack;
    public Material[] backPackMaterials;

    bool canPickUp;
    string typeOfEgg;
    float CamoHatTime;
    float speedBoostTime;
    float slowMotionTime;
    float timeLeft;
    bool usingTimer;


    void Awake ()
    {
        canPickUp = true;
        CamoHatTime = 8;
        speedBoostTime = 15;
        slowMotionTime = 10;
    }

    void Update ()
    {
        if (usingTimer == false && canPickUp == false && (Input.GetButtonDown("L2") || Input.GetKeyDown(KeyCode.Space)))
        {
            switch (typeOfEgg)
            {
                case "Camouflage Hat":
                    Camouflage_Hat(true);
                    timeLeft = CamoHatTime;
                    usingTimer = true;
                    break;
                case "Speed Boost":
                    Speed_Boost(true);
                    timeLeft = speedBoostTime;
                    usingTimer = true;
                    break;
                /*case "Gold Coin":
                    GoldCoin(true);
                    break;*/
                case "Slow Motion":
                    Slow_Motion(true);
                    timeLeft = slowMotionTime;
                    usingTimer = true;
                    break;
            }
        }

        if (usingTimer == true)
        {
            timeLeft -= Time.deltaTime;
            //decrease pickup effect backpack indicator
            if (timeLeft <= 0)
            {
                switch (typeOfEgg)
                {
                    case "Camouflage Hat":
                        Camouflage_Hat(false);
                        break;
                    case "Speed Boost":
                        Speed_Boost(false);
                        break;
                    case "Slow Motion":
                        Slow_Motion(false);
                        break;
                }
            }
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (canPickUp == true && collision.gameObject.CompareTag("Egg"))
        {
            Egg eggScript = collision.gameObject.GetComponent<Egg>();
            typeOfEgg = eggScript.typeOfEgg;
            canPickUp = false;
            //print("got egg of type:  " + typeOfEgg);
            switch (typeOfEgg)
            {
                case "Camouflage Hat":
                    backPack.GetComponent<Renderer>().material = backPackMaterials[1];
                    break;
                case "Speed Boost":
                    backPack.GetComponent<Renderer>().material = backPackMaterials[2];
                    break;
                case "Gold Coin":
                    backPack.GetComponent<Renderer>().material = backPackMaterials[3];
                    GoldCoin(true);
                    break;
                case "Slow Motion":
                    backPack.GetComponent<Renderer>().material = backPackMaterials[4];
                    break;
            }

            Destroy(collision.gameObject);
        }
    }
    

    void Camouflage_Hat(bool enable)
    {
        if (enable == true)
        {
            this.gameObject.tag = "Hidden_Player";
        }
        else if (enable == false)
        {
            this.gameObject.tag = "Player";
            canPickUp = true;
            usingTimer = false;
            backPack.GetComponent<Renderer>().material = backPackMaterials[0];
        }
    }

    public void GoldCoin(bool enable)
    {
        if (enable == true)
        {
            hasGoldCoin = true;//add spawning a gold coin at the top of the gun
        }
        else if (enable == false)
        {
            hasGoldCoin = false;
            canPickUp = true;
            usingTimer = false;
            backPack.GetComponent<Renderer>().material = backPackMaterials[0];
        }
    }

    void Speed_Boost(bool enable)
    {
        if (enable == true)
        {
            //Player_Controller_Movement pCmS = transform.parent.GetComponent<Player_Controller_Movement>();
            Player_Keyboard_Movement pCmS = GetComponent<Player_Keyboard_Movement>();
            pCmS.MovementSpeed *= 2;
            //add attack speed
        }
        else if (enable == false)
        {
            //Player_Controller_Movement pCmS = transform.parent.GetComponent<Player_Controller_Movement>();
            Player_Keyboard_Movement pCmS = GetComponent<Player_Keyboard_Movement>();
            pCmS.MovementSpeed /= 2;
            //add attack speed
            canPickUp = true;
            usingTimer = false;
            backPack.GetComponent<Renderer>().material = backPackMaterials[0];
        }
    }

    void Slow_Motion(bool enable)
    {
        if (enable == true)
        {
            Time.timeScale = 0.3f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        else if (enable == false)
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            canPickUp = true;
            usingTimer = false;
            backPack.GetComponent<Renderer>().material = backPackMaterials[0];
        }
    }
}