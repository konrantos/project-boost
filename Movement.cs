using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Ρυθμίσεις για το thurst και το rotation του κινητήρα
    [SerializeField] float mainThurst = 1000f; 
    [SerializeField] float rotationThurst = 1f; 
    [SerializeField] AudioClip engineThurst;// Ήχος κινητήρα
    [SerializeField] ParticleSystem mainBoosterParticle;// Κύριο σύστημα particles
    [SerializeField] ParticleSystem leftBoosterParticle; // Particles αριστερού booster
    [SerializeField] ParticleSystem rightBoosterParticle;// Particles δεξιού booster

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();// Παίρνουμε το Rigidbody
        audioSource = GetComponent<AudioSource>();// Παίρνουμε το AudioSource
    }

 void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }


    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThursting();

        }

        else
        {
            StopThursting();
        }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();

        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }

        else
        {
            StopRotating();
        }

    }

    void StartThursting()
    {
        rb.AddRelativeForce(Vector3.up * mainThurst * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engineThurst);
        }


        if (!mainBoosterParticle.isPlaying)
        {
            mainBoosterParticle.Play();
        }
    }

    private void StopThursting()
    {
        audioSource.Stop();
        mainBoosterParticle.Stop();
    }
   
    private void RotateLeft()
    {
        ApplyRotation(rotationThurst);

        if (!rightBoosterParticle.isPlaying)
        {
            rightBoosterParticle.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThurst);

        if (!leftBoosterParticle.isPlaying)
        {
            leftBoosterParticle.Play();
        }
    }
 
    private void StopRotating()
    {
        rightBoosterParticle.Stop();
        leftBoosterParticle.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
        {
            rb.freezeRotation = true;  
            transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
            rb.freezeRotation = false; 
        }

}
