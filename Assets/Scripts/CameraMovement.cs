using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Follow_Target; // allows you to put a game object in here to follow
    public float movespeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Follow_Target != null)
        {
            // has the camera follow behind the targeted object
            transform.position = Vector3.Lerp(transform.position,
            Follow_Target.transform.position, Time.deltaTime * movespeed);
        }
    }
}
