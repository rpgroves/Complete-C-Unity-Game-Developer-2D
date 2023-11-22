using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTrails : MonoBehaviour
{
    [SerializeField] ParticleSystem dustTrails;
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Ground")
        {
            dustTrails.Play();
        }    
    }

    void OnCollisionExit2D(Collision2D other) 
    {
        dustTrails.Stop();
    }
}
