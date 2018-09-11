using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberConfig : MonoBehaviour
{
    public GameObject[] members;

    public float maxFOV = 180;
    public float maxVelocity;

    //cohesion
    public float cohesionRadius;
    public float cohesionPriority;

    //alignment
    public float alignmentRadius;
    public float alignmentPriority;

    //seperation 
    public float seperationRadius;
    public float seperationPriority;

    //avoidance
    public float avoidanceRadius;
    public float avoidancePriority;

    //follow
    public float followRadius;
    public float followPriority;

    public void Update()
    {
        members = GameObject.FindGameObjectsWithTag("Enemy");
    }
}
