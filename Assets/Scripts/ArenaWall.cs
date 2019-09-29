using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaWall : MonoBehaviour {

    private Animator arenaAnimator;

    // Use this for initialization
    void Start ()
    {
        GameObject arena = transform.parent.gameObject; // gets the parent child 
        arenaAnimator = arena.GetComponent<Animator>(); // calls for a reference of the animator 

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other) 
    {
        arenaAnimator.SetBool("IsLowered", true); // when hero enters collider is triggered sets islowered to true 
    }

    void OnTriggerExit(Collider other)
    {
        arenaAnimator.SetBool("IsLowered", false); // when hero leaves collider sets islowered to false
    }

}
