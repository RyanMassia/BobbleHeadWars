using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject followTarget; // allows you to put a game object in here to follow
    public float moveSpeed;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (followTarget != null)
        {
            // has the camera follow behind the targeted object
            transform.position = Vector3.Lerp(transform.position, followTarget.transform.position, Time.deltaTime * moveSpeed);
        }
	}
}
