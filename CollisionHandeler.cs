using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandeler : MonoBehaviour
{
    // Delay πριν το φόρτωμα του επόμενου επιπέδου
    [SerializeField] float levelLoadDelay = 1f;
    // Ήχος σύγκρουσης
    [SerializeField] AudioClip collisionClip;
    // Ήχος για το πέρασμα στο επόμενο επίπεδο
    [SerializeField] AudioClip nextLevelClip;

    // Particles σύγκρουσης
    [SerializeField] ParticleSystem collisionParticle;
    // Particles για την επιτυχία του επιπέδου
    [SerializeField] ParticleSystem nextLevelParticle;

    AudioSource audioSource;
    Movement mv;

    // Μεταβλητές για τον έλεγχο της κατάστασης των transitions και των συγκρούσεων
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mv = GetComponent<Movement>();
    }
    
    void Update()
    {
        RespondToDebugKeys();
    }

    // Έλεγχος για τα πλήκτρα ενεργοποίησης του debug
    void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            collisionDisabled =! collisionDisabled;
        }
    }

    // Λειτουργία κατά την σύγκρουση
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }

        switch(other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartsSucessSequence();
                break;
            default:
                StartCrashSequence();  
                break;
        }
    }

    // Ξεκινά τη σειρά ενεργειών για σύγκρουση
    void StartCrashSequence()
    { 
        isTransitioning = true;
        audioSource.Stop();  
        audioSource.PlayOneShot(collisionClip);
        collisionParticle.Play();
        mv.enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    // Ξεκινά τη σειρά ενεργειών για την επιτυχία επιπέδου
    void StartsSucessSequence()
    { 
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(nextLevelClip);
        nextLevelParticle.Play();
        mv.enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    // Φορτώνει το επόμενο επίπεδο
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    // Επαναφορτώνει το τρέχον επίπεδο
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
