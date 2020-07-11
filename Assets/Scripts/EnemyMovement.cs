using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rigidBody;

    Vector2 movement;

    // Update is called once per frame
    
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

    }

    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.name.Equals("Hitbox"))
        {
            Debug.Log("I'll leave now");
            Destroy(gameObject);
        }
    }
}
