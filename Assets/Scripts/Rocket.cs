using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [Header ("Velocity info")]
    [SerializeField] private float mainThrust;
    [Header ("Rotation info")]
    [SerializeField] private float rotationThrust;

    private Rigidbody rigidBody;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();       
    }


    private void ProcessThrust()
    {
        if (Input.GetButton("Jump"))
        {
            if (!audioSource.isPlaying)
            {
               audioSource.Play();
            }
            rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
        else
        {
            audioSource.Pause();
        }
    }
    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThrust * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            ResetPlayerPosition();            
            StartCoroutine(SetDefaultRBvalues());
        }
    }

    private void ResetPlayerPosition()
    {
        rigidBody.freezeRotation = true;
        rigidBody.drag = 1000;
        transform.position = new Vector3(-34, 8f, 2.54f);
        transform.rotation = Quaternion.identity;
    }

    private IEnumerator SetDefaultRBvalues()
    {
        yield return new WaitForSeconds(0.2f);
        rigidBody.freezeRotation = false;
        rigidBody.drag = 0;
    }
}
