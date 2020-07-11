using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kidMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rB;

    public PolygonCollider2D collider;

    Vector2 movement;

    private void OnTriggerEnter2D(Collider2D collider){
    	Debug.Log("OH MY GOD!");
    }

    void Update()
    {
      	

    }
    void FixedUpdate()
    {
        
    }
}
