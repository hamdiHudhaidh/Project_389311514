using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;

	void Start ()
    {
        offset = transform.position - target.position;
    }
	
	void FixedUpdate ()
    {
        Vector3 targetCameraPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, smoothing * Time.deltaTime);
	}
}
