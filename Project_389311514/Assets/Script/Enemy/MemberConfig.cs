using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberConfig : MonoBehaviour
{
    public float maxFOV = 180;
    public float maxAcceleration;
    public float maxVelocity;

    //wander
    public float wanderJitter;
    public float wanderRadius;
    public float wanderDistance;
    public float wanderPriority;

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
}
