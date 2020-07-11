using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public WorldScript worldScript;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.name.Equals("Player"))
        {
            Debug.Log("Game is done");
            worldScript.ping();
        }
    }
}
