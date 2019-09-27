using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50.0f;
    private CharacterController characterController;
    public Rigidbody head; // so we can apply force to the head
    public LayerMask layerMask; // where the ray will hit
    private Vector3 currentLookTarget = Vector3.zero; //look value starts at 0
    public Animator bodyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // old code for the rididbody
        /*  Vector3 pos = transform.position;
        pos.x += movespeed * Input.GetAxis("Horizontal") * Time.deltaTime; //allows the player to move along the x  and z axix with their set speed
        pos.z += movespeed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position = pos;
       */

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), //stores movement direction of the player
                                           0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection * moveSpeed); //allows charcter to move but not through objects
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),
                                           0, Input.GetAxis("Vertical"));
        if (moveDirection == Vector3.zero) //if vector 3 is zero player isnt moving
        {
            bodyAnimator.SetBool("IsMoving", false);
        }
        else
        {
            head.AddForce(transform.right * 150, ForceMode.Acceleration); //head moves by set amount
            bodyAnimator.SetBool("IsMoving", true);
        }

        RaycastHit hit; //creates empty raycast , if it hits something it will fill
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //cast ray from camera to mouse spot
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);//shows ray in unity
        // casts the ray, sends it out, goes out 1000m, hits layermask, telling ray to ignore triggers
        if (Physics.Raycast(ray, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
        {
        }
        //1. fet the target position so that marine looks straight
        Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        //2. determines rotation by solving Quaternion by subtracting target position from current position 
        Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
        //3.does rotation smoothly over the time 
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10.0f);
    }
}