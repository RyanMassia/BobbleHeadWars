using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Alien : MonoBehaviour {

    public Transform target;
    public float navigationUpdate; // tracks in miliseconds when alien should update path
    public UnityEvent OnDestroy;
    private float navigationTime = 0; // tracks how much time has passed since last update
    private NavMeshAgent agent;

    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        navigationTime += Time.deltaTime;
        if (navigationTime > navigationUpdate) //cheaks to see if a certain amount of time has passed
        {
            agent.destination = target.position;
            navigationTime = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
        //Destroy(gameObject); // objext is destroyed when colliding with another collider
        Die(); // triggers die function
    }

    public void Die()
    {
        OnDestroy.Invoke();
        OnDestroy.RemoveAllListeners();
        Destroy(gameObject);
    }
}
