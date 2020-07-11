using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{

    public WorldScript worldScript;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.name.Equals("Player"))
        {
            worldScript.load("MainMenu");
        }
        else if(collider.name.Equals("Enemy") || collider.name.Equals("StoneEnemy"))
        {
            EnemyMovement enemy = collider.transform.gameObject.GetComponent<EnemyMovement>();
            enemy.destroySelf();
        }

    }
}
