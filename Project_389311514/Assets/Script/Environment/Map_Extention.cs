using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Extention : MonoBehaviour
{
    public void OpenArea()//called when the lock is opened to destroy the lock
    {
        //add deslove effect
        Destroy(this.gameObject);
    }
}
