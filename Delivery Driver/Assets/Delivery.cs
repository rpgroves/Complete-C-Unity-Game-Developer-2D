using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    bool hasPackage = false;
    [SerializeField] float timeToDestroy = 0.1f;
    [SerializeField] Color32 hasPackageColor = new Color32 (1, 1, 1, 1);
    [SerializeField] Color32 noPackageColor = new Color32 (1, 1, 1, 1);

    SpriteRenderer spriteRenderer;

    void Start() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("Bump!");
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Package" && hasPackage == false)
        {
            Debug.Log("Package Picked Up!");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(other.gameObject, timeToDestroy);
        }
        else if(other.tag == "Customer" && hasPackage == true)
        {
            Debug.Log("Delivered To Customer!");  
            hasPackage = false;
            spriteRenderer.color = noPackageColor;
        }  
        else
            Debug.Log("Triggered >:( ");  

    }
}
