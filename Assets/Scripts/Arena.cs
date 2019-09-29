using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    public GameObject player;
    public Transform elevator;
    private Animator arenaAnimator; // will kick off the animation 
    private SphereCollider sphereCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        arenaAnimator = GetComponent<Animator>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Camera.main.transform.parent.gameObject.
        GetComponent<CameraMovement>().enabled = false;
        player.transform.parent = elevator.transform;
        player.GetComponent<PlayerController>().enabled = false; // stops the player from turning and shooting
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.elevatorArrived);
        arenaAnimator.SetBool("OnElevator", true); // starts animation
    }

    public void ActivatePlatform()
    {
        sphereCollider.enabled = true;
    }
}
