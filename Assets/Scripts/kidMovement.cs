using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kidMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rB;

    Vector2 movement;

    private void OnTriggerEnter2D(Collider2D collider)
    {
    	reactToCollision(collider);

    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        reactToCollision(collision.collider);   
    }

    private void reactToCollision(Collider2D collider)
    {
        Debug.Log("OH MY GOD!");
        Vector2 enemyPosition = collider.transform.gameObject.transform.position;

        float deltaX = rB.position.x - enemyPosition.x;
        float deltaY = rB.position.y - enemyPosition.y;

        if(Mathf.Abs(deltaX) >= Mathf.Abs(deltaY))
        {
            if(deltaX > 0)  Debug.Log("I should go right");
            else Debug.Log("I should go left");
        }
        else
        {
            if(deltaY > 0)  Debug.Log("I should go up");
            else Debug.Log("I should go down");
        }
    }

    void Update()
    {
      	

    }
    void FixedUpdate()
    {
        
    }
}
