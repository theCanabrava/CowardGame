using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleBehaviour : MonoBehaviour
{
    public WorldScript worldScript;
    public bool holeActive = false;
    public bool timoutStarted = false;

    private List<Collider2D> within = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(holeActive)
        {
            if(collider.name.Equals("Player"))
            {
                worldScript.load("MainMenu");
            }
            else if(collider.name.Equals("Enemy"))
            {
                EnemyMovement enemy = collider.transform.gameObject.GetComponent<EnemyMovement>();
                enemy.destroySelf();
            }    
        }
        else
        {
            within.Add(collider);
        }
        /*
        
        */

    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        within.Remove(collider);
    }
}
