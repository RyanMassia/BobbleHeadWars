using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Alien : MonoBehaviour {

    public Transform target;
    public float navigationUpdate; // tracks in miliseconds when alien should update path
    public UnityEvent OnDestroy;
    public Rigidbody head;
    public bool isAlive = true;
    private float navigationTime = 0; // tracks how much time has passed since last update
    private NavMeshAgent agent;
    private DeathParticles deathParticles;

    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        navigationTime += Time.deltaTime;

        if (isAlive)
        {
            if (navigationTime > navigationUpdate) //cheaks to see if a certain amount of time has passed
            {
                agent.destination = target.position;
                navigationTime = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isAlive)
        {
            Die();
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
        }        
        //Destroy(gameObject); // objext is destroyed when colliding with another collider
        //Die(); // triggers die function
    }

    public void Die()
    {
        isAlive = false;
        head.GetComponent<Animator>().enabled = false;
        head.isKinematic = false;
        head.useGravity = true;
        head.GetComponent<SphereCollider>().enabled = true;
        head.gameObject.transform.parent = null;
        head.velocity = new Vector3(0, 26.0f, 3.0f);
        OnDestroy.Invoke();
        OnDestroy.RemoveAllListeners();
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
        head.GetComponent<SelfDestruct>().Initiate();
        if (deathParticles)
        {
            deathParticles.transform.parent = null;
            deathParticles.Activate();
        }
        Destroy(gameObject);
    }
    public DeathParticles GetDeathParticles()
    {
        if (deathParticles == null)
        {
            deathParticles = GetComponentInChildren<DeathParticles>();//  returns the first death particle script that it finds
        }
        return deathParticles;
    }
}
