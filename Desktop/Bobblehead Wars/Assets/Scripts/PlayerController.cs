using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 50.0f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += movespeed * Input.GetAxis("Horizontal") * Time.deltaTime; //allows the player to move along the x  and z axix with their set speed
        pos.z += movespeed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position = pos; 
    }
}
