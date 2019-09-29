using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);  // destroys object if it isnt seen on screen
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); // destroys object when it collides with another object
    }
}
