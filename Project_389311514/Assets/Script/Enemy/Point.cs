using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public float accShortDist;
    public Point from;
    public bool visited;
    public bool doneWithPoint;

	void Start ()
    {
        accShortDist = float.MaxValue;
        visited = false;
        doneWithPoint = false;
	}
	
	void Update ()
    {
		
	}
}
