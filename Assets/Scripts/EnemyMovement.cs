using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rigidBody;

    public WorldScript worldScript;
    Vector2 movement;

    public bool enableFlag = false;

    // Update is called once per frame

    void Start()
    {
        worldScript.addEnemy(this);
    }
    
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if(enableFlag)
        {
            rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.name.Equals("Hitbox")) destroySelf();
    }

    public void destroySelf()
    {
        worldScript.dropEnemy(this);
        Destroy(gameObject);
    }

    private void OnMouseUp() 
    {
        worldScript.disableEnemies();
        enableFlag = true;
    }
}
