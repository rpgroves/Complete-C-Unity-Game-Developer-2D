using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D myRigidBody;
    BoxCollider2D myBoxCollider;
    float moveDirection = 1f;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        myRigidBody.velocity = new Vector2 (moveSpeed * moveDirection, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag != "Coin")
        {
            transform.localScale = new Vector2 (-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
            moveDirection = -moveDirection;
        }
    }
}
