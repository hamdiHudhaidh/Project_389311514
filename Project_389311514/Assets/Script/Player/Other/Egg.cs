using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public string typeOfEgg;
    public Renderer renderer;
    public Material[] eggMaterials;

    float timeLeft;

    void Start ()
    {
        renderer = GetComponent<Renderer>();
        timeLeft = 15;
        int temp = Random.Range(1, 5);
        switch (temp)
        {
            case 1:
                typeOfEgg = "Camouflage Hat";
                renderer.material = eggMaterials[0];
                break;
            case 2:
                typeOfEgg = "Speed Boost";
                renderer.material = eggMaterials[1];
                break;
            case 3:
                typeOfEgg = "Gold Coin";
                renderer.material = eggMaterials[2];
                break;
            case 4:
                typeOfEgg = "Slow Motion";
                renderer.material = eggMaterials[3];
                break;
        }

        //disapear over time
        Destroy(this.gameObject, 15);
    }
}