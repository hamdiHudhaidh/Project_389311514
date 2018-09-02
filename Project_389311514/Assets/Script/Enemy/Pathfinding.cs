using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public GameObject player;

    public Transform[] points;

    float distanceToSelf = 0;
    float distanceFromTarget = 0;
    Transform startPoint;
    Transform endPoint;

    void Start ()
    {
        GetStartAndEnd();
    }
	
	void Update ()
    {
        
	}

    public void FindPath()
    {
        GetStartAndEnd();

    }

    void GetStartAndEnd()
    {
        for (int i = 0; i < points.Length; i++)
        {
            //start
            if (distanceToSelf <= 0)//to start
            {
                distanceToSelf = Vector3.Distance(this.transform.position, points[i].position);
                startPoint = points[i];
            }
            else if (Vector3.Distance(this.transform.position, points[i].position) < distanceToSelf)
            {
                distanceToSelf = Vector3.Distance(this.transform.position, points[i].position);
                startPoint = points[i];
            }

            //end
            if (distanceFromTarget <= 0)//to start
            {
                distanceFromTarget = Vector3.Distance(player.transform.position, points[i].position);
                endPoint = points[i];
            }
            else if (Vector3.Distance(player.transform.position, points[i].position) < distanceFromTarget)
            {
                distanceFromTarget = Vector3.Distance(player.transform.position, points[i].position);
                endPoint = points[i];
            }
        }

        //print(startPoint.gameObject.name + "  " + endPoint.gameObject.name);////
    }
}
