﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public WorldScript worldScript;

    private void OnMouseUp() 
    {
        
        worldScript.load("SampleScene");

    }
}