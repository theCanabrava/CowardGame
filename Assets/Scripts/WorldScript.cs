using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldScript : MonoBehaviour
{
    public List<EnemyMovement> enemies = new List<EnemyMovement>();
    public int level;

    public void addEnemy(EnemyMovement enemy)
    {
        enemies.Add(enemy);
        disableEnemies();
    }

    public void disableEnemies()
    {
        foreach(EnemyMovement enemy in enemies)
        {
            enemy.enableFlag = false;
        }
    }

    public void dropEnemy(EnemyMovement enemy)
    {
        enemies.Remove(enemy);
        if(enemies.Count == 0)
        {
            load("MainMenu");
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

    public void nextLevel()
    {
        SceneManager.LoadScene((level+1)%2);
    }


}
