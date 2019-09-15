using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 50.0f;
    private CharacterController characterController;
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
        characterController.SimpleMove(moveDirection * movespeed); //allows charcter to move but not through objects
    }
}
