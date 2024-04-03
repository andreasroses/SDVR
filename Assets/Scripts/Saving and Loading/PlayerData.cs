using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerData
{
    // General player data, items, stats, etc.
    public int exampleData_1;
    public int exampleData_2;
    public string exampleData_3;
    
    System.Random rnd = new System.Random();
    public PlayerData ()
    {
        // GeneralInfo health, level, exp, etc. getters go here, grabbing all necessary data to save
        // health = PlayerHealth.currentHealth;
        
        exampleData_1 = rnd.Next(999);
        exampleData_2 = rnd.Next(999);
        exampleData_3 = "Saving Example Data";

    }
    
}
