using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform launch_position;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //if button is pressed will go into loop
        {
            if (!IsInvoking("fireBullet"))
            {
                InvokeRepeating("fireBullet", 0f, 0.1f); // valls the fire bullet function till button is no longeer pressed
            }
  
        }
        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("fireBullet"); // when released function will stop
        }
    }
    void fireBullet()
    {
        GameObject bullet = Instantiate(BulletPrefab) as GameObject; 
        bullet.transform.position = launch_position.position;
        bullet.GetComponent<Rigidbody>().velocity = transform.parent.forward * 100; // bullets movement speed
    }
}