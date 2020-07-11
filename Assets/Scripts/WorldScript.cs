using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldScript : MonoBehaviour
{
    public List<EnemyMovement> enemies = new List<EnemyMovement>();

    public void addEnemy(EnemyMovement enemy)
    {
        Debug.Log("Adding: " + enemy);
        enemies.Add(enemy);
        disableEnemies();
    }

    public void disableEnemies()
    {
        Debug.Log("Reseting control");
        foreach(EnemyMovement enemy in enemies)
        {
            Debug.Log("Disableing: " + enemy.name);
            enemy.enableFlag = false;
        }
    }

    public void ping()
    {
        Debug.Log("My children are calling me!");
    }

    public void load(string level)
    {
        SceneManager.LoadScene(level);
    }
}
