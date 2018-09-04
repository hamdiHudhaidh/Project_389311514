using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public float accShortDist;
    public Transform from;
    public bool visited;

	void Start ()
    {
        accShortDist = float.MaxValue;
        visited = false;
	}
	
	void Update ()
    {
		
	}
}
