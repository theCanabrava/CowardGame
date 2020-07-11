using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldScript : MonoBehaviour
{
    public void ping()
    {
        Debug.Log("My children are calling me!");
    }

    public void load(string level)
    {
        SceneManager.LoadScene(level);
    }
}
