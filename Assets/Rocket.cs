using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using AudioSource = UnityEngine.AudioSource;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;

	// Use this for initialization
	void Start ()
	{
	    audioSource = GetComponent<AudioSource>();
	    rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space)) //can thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            
        }
        else
        {
            audioSource.Stop();
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }
    }
}
