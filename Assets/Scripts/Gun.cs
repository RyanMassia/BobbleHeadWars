using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform launch_position;
    public bool isUpgraded;// lets the script know wether it fire 1 or 3 bullets
    public float upgradeTime = 10.0f; // how long the upgrade lasts 

    private float currentTime; // keeps track of when gun was upgraded
    private AudioSource audioSource; 
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
   
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > upgradeTime && isUpgraded == true)
        {
            isUpgraded = false;
        }

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
        Rigidbody bullet = createBullet();
        bullet.velocity = transform.parent.forward * 100;

        if (isUpgraded)
        {
            Rigidbody bullet2 = createBullet();
            bullet2.velocity =
            (transform.right + transform.forward / 0.5f) * 100;
            Rigidbody bullet3 = createBullet();
            bullet3.velocity =
            ((transform.right * -1) + transform.forward / 0.5f) * 100;
        }

        if (isUpgraded)
        {
            audioSource.PlayOneShot(SoundManager.Instance.upgradedGunFire);
        }
        else
        {
            audioSource.PlayOneShot(SoundManager.Instance.gunFire);
        }
    }

    private Rigidbody createBullet()
    {
        GameObject bullet = Instantiate(BulletPrefab) as GameObject;
        bullet.transform.position = launch_position.position;
        return bullet.GetComponent<Rigidbody>();
    }

    public void UpgradeGun()
    {
        isUpgraded = true;
        currentTime = 0;
    }
}