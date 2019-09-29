using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50.0f;
    public Rigidbody head;
    public LayerMask layerMask;
    public Animator bodyAnimator;
    public float[] hitForce;
    public float timeBetweenHits = 2.5f; // period before hero takes damage
    public Rigidbody marineBody;
    private bool isDead = false; // keeps track of the player’s current death state
    private bool isHit = false; // says if hero took a hit
    private float timeSinceHit = 0; // tracks grace time since hit
    private int hitNumber = -1; // number of times hero hit 
    private Vector3 currentLookTarget = Vector3.zero;
    private CharacterController characterController;
    private DeathParticles deathParticles;

    // Use this for initialization
    void Start ()
    {
        characterController = GetComponent<CharacterController>();
        deathParticles = gameObject.GetComponentInChildren<DeathParticles>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));  //stores movement direction of the player
        characterController.SimpleMove(moveDirection * moveSpeed); //allows charcter to move but not through objects
        if (isHit)
        {
            timeSinceHit += Time.deltaTime;
            if (timeSinceHit > timeBetweenHits)
            {
                isHit = false;
                timeSinceHit = 0;
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (moveDirection == Vector3.zero) //if vector 3 is zero player isnt moving
        {
            bodyAnimator.SetBool("IsMoving", false);
        }
        else
        {
            head.AddForce(transform.right * 150, ForceMode.Acceleration); //head moves by set amount
            bodyAnimator.SetBool("IsMoving", true);
        }
        RaycastHit hit;//creates empty raycast , if it hits something it will fill
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//cast ray from camera to mouse spot
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);//shows ray in unity
        // casts the ray, sends it out, goes out 1000m, hits layermask, telling ray to ignore triggers
        if (Physics.Raycast(ray, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.point != currentLookTarget)
            {
                currentLookTarget = hit.point;
            }
        }
        Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);  //1. fet the target position so that marine looks straight
        Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position); //2. determines rotation by solving Quaternion by subtracting target position from current position 
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10.0f); //3.does rotation smoothly over the time 
    }

    void OnTriggerEnter(Collider other)
    {
        Alien alien = other.gameObject.GetComponent<Alien>();
        if (alien != null)
        { // 1: if it’s an alien and the player hasn’t been hit, then the player is officially considered hit
            if (!isHit)
            {
                hitNumber += 1; // 2:  increases by one
                CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
                if (hitNumber < hitForce.Length) // 3  if less then current camera shakes hero is still alive
                {
                    cameraShake.intensity = hitForce[hitNumber];
                    cameraShake.Shake();
                }
                else
                {
                    Die();
                }
                isHit = true; // 4: plays the grunt sound and kills the alien
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.hurt);
            }
            alien.Die();
        }
    }
    public void Die()
    {
        bodyAnimator.SetBool("IsMoving", false);
        marineBody.transform.parent = null;
        marineBody.isKinematic = false; // player cant move the corpse 
        marineBody.useGravity = true; // falls to the ground 
        marineBody.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        marineBody.gameObject.GetComponent<Gun>().enabled = false; // prevents the player from firing after death.
        Destroy(head.gameObject.GetComponent<HingeJoint>()); // destroys the joint to release the head from the body
        head.transform.parent = null; // Now that head can roll!
        head.useGravity = true; // 
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.marineDeath);
        deathParticles.Activate();
        Destroy(gameObject);
    }
}
