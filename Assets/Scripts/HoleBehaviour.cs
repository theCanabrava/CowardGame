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
        Debug.Log("Hey");
        if(holeActive)
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
        else
        {
            Debug.Log("Sup");
            within.Add(collider);
            StartCoroutine(ExampleCoroutine());
        }
    }

    IEnumerator ExampleCoroutine()
    {
        Debug.Log("Started timeout");
        timoutStarted = true;
        yield return new WaitForSeconds(2);
        Debug.Log("Timed out");
        holeActive = true;
        destroyOnList();
    }

    private void destroyOnList()
    {
        foreach(Collider2D collider in within)
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

    private void OnTriggerExit2D(Collider2D collider) 
    {
        within.Remove(collider);
    }
}
