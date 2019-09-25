using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaWall : MonoBehaviour
{
    private Animator arenaAnimator;
    // Start is called before the first frame update
    void Start()
    {
        GameObject arena = transform.parent.gameObject; // gets the parent child 
        arenaAnimator = arena.GetComponent<Animator>(); // calls for a reference of the animator 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        arenaAnimator.SetBool("islowered", true); // when hero enters collider is triggered sets islowered to true 
    }

    private void OnTriggerExit(Collider other)
    {
        arenaAnimator.SetBool("islowered", false); // when hero leaves collider sets islowered to false
    }
}
