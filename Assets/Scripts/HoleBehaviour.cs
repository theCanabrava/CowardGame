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
        if(holeActive) throwOnHole(collider);
        else putOnTrap(collider);
    }

    private putOnTrap(Collider2D collider)
    {
        within.Add(collider);
        StartCoroutine(TrapCountdown());
    }

    private void throwOnHole(Collider2D collider)
    {
        if(collider.name.Equals("Player")) worldScript.gameOver();
        else if(collider.name.Equals("Enemy") || collider.name.Equals("StoneEnemy")) destroyEnemy(collider);
    }

    private void destroyEnemy(Collider2D collider)
    {
        EnemyMovement enemy = collider.transform.gameObject.GetComponent<EnemyMovement>();
        enemy.destroySelf();
    }


    IEnumerator TrapCountdown()
    {
        if(!timoutStarted)
        {
            timoutStarted = true;
            yield return new WaitForSeconds(2);
            holeActive = true;
            destroyOnList();
        }
    }

    private void destroyOnList()
    {
        foreach(Collider2D collider in within) throwOnHole(collider);
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        within.Remove(collider);
    }
}
