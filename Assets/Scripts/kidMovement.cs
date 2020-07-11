﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kidMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rB;

    Vector2 movement;

    flag True;

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
        if(!collider.name.Equals("Hitbox") && !collider.name.Equals("Door"))
        {
            Vector2 enemyPosition = collider.transform.gameObject.transform.position;

            float deltaX = rB.position.x - enemyPosition.x;
            float deltaY = rB.position.y - enemyPosition.y;

            if(Mathf.Abs(deltaX) >= Mathf.Abs(deltaY))
            {
                if(deltaX > 0) moveFromScare(new Vector2(1, 0));
                else moveFromScare(new Vector2(-1, 0));
            }
            else
            {
                if(deltaY > 0) moveFromScare(new Vector2(0, 1));
                else moveFromScare(new Vector2(0, -1));
            }
        }
    }

    private void moveFromScare(Vector2 direction)
    {
        Debug.Log("Moved");
        rB.velocity = new Vector2(160*direction.x, 160*direction.y);
        rB.MoveRotation(90*direction.x + (90 + 90*direction.y)*direction.y);
    }

    private void initialMovement(){

		Vector2 startPosition

		transform.

    }

    void Update()
    {
      	

    }
    void FixedUpdate()
    {
        
    }
}
