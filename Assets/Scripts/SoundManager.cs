﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;
    public AudioClip gunFire;
    public AudioClip upgradedGunFire;
    public AudioClip hurt;
    public AudioClip alienDeath;
    public AudioClip marineDeath;
    public AudioClip victory;
    public AudioClip elevatorArrived;
    public AudioClip powerUpPickup;
    public AudioClip powerUpAppear;
    private AudioSource soundEffectAudio;  // references audio source 

    // Use this for initialization
    void Start ()
    {
        if (Instance == null)
        {
            Instance = this; // creates a instance of a sound mananger
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // if for some reason another instance is made it is destroyed
        }
        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.clip == null)
            {
                soundEffectAudio = source;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PlayOneShot(AudioClip clip)
    {
        soundEffectAudio.PlayOneShot(clip);
    }

}
