using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] ParticleSystem deathEffect;
    [SerializeField] float loadDelay = 1.5f;
    [SerializeField] AudioClip crashSound;

    bool hasPlayedSFX = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ground" && !hasPlayedSFX) 
        {
            FindObjectOfType<PlayerController>().DisableControls();
            deathEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSound);
            Invoke("ReloadScene", loadDelay);
            hasPlayedSFX = true;
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}