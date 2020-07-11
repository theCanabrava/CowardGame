using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kidMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float waitTime;
    public float startWaitTime;

    public Rigidbody2D rB;

    public Transform[ ] moveSpots;
    private int randomSpot;

    Vector2 movement;

   // bool flag = true;

    void Start(){

        waitTime = startWaitTime;
        randomSpot = 0;
    }

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
        if(!collider.name.Equals("Hitbox") && !collider.name.Equals("Door") && !collider.name.Equals("Hole"))
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

    void Update()
    {
      	
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f){
            if (waitTime <= 0){
                    randomSpot = Random.Range(0, 3);
                    waitTime = startWaitTime;
                } else {
                    waitTime -= Time.deltaTime;
                }
        }
    }
    void FixedUpdate()
    {
        
    }
}
