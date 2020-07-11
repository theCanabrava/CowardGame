using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneEnemyMovement : EnemyMovement
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("I am made of stone");
        if(collider.name.Equals("Hitbox") && enableFlag)
        {
            worldScript.dropEnemy(this);
            Destroy(gameObject);
        }
    }
}
