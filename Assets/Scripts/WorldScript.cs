using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldScript : MonoBehaviour
{
    public List<EnemyMovement> enemies = new List<EnemyMovement>();
    public int level;

    int savedLevel = 1;

    private void Start() 
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            savedLevel = PlayerPrefs.GetInt("SavedLevel");
        }
        else savedLevel = 1; 
        if(savedLevel == 0) savedLevel = 1;
        else if(savedLevel == 11) savedLevel = 1;
        Debug.Log(savedLevel);
    }

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
        if(enemies.Count == 0) gameOver();
    }


    public void load(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void startGame()
    {
        SceneManager.LoadScene(savedLevel);
    }

    public void nextLevel()
    {
        PlayerPrefs.SetInt("SavedLevel", (level+1)%12);
	    PlayerPrefs.Save();
        SceneManager.LoadScene((level+1)%12);
    }

    public void gameOver()
    {
        load("MainMenu");
    }


}
