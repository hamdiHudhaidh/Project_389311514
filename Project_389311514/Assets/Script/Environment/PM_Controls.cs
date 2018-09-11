using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PM_Controls : MonoBehaviour
{
    public Button[] buttons;
    Color green = Color.green;
    Color white = Color.white;
    bool canPress;
    Button currentButton;
    int buttonIndicator;
    float first = 0;

    void Start ()
    {
        Image img = buttons[0].GetComponent<Image>();
        img.color = green;
        currentButton = buttons[0];

        CanPress();
    }
	
	void Update ()
    {
        if (Input.GetButtonUp("X"))
        {
            currentButton.onClick.Invoke();
        }

        if (canPress == false)
        {
            //timer
            if (first < 1)
            {
                first = Time.realtimeSinceStartup + 0.2f;
            }
            if (Time.realtimeSinceStartup > first)
            {
                CanPress();
            }
        }

        if (Input.GetAxis("DpadV") == 1 && canPress == true)//up
        {
            canPress = false;

            buttonIndicator--;
            if (buttonIndicator == -1)
            {
                buttonIndicator = 2;
            }
            currentButton = buttons[buttonIndicator];
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i] == currentButton)
                {
                    Image img = buttons[i].GetComponent<Image>();
                    img.color = green;
                }
                else
                {
                    Image img = buttons[i].GetComponent<Image>();
                    img.color = white;
                }
            }
        }
        else if (Input.GetAxis("DpadV") == -1 && canPress == true)//down
        {
            canPress = false;
            Invoke("CanPress", 1f);
            buttonIndicator++;
            if (buttonIndicator == 3)
            {
                buttonIndicator = 0;
            }
            currentButton = buttons[buttonIndicator];
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i] == currentButton)
                {
                    Image img = buttons[i].GetComponent<Image>();
                    img.color = green;
                }
                else
                {
                    Image img = buttons[i].GetComponent<Image>();
                    img.color = white;
                }
            }
        }
	}

    void CanPress()
    {
        canPress = true;
        first = 0;
    }
}
