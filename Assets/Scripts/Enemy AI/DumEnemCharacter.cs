using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Kitbashery.Gameplay;
using System;
using System.Collections;
using UnityEngine.UI;

public class DumEnemCharacter : Entity
{
    int maxEmHitpoints;
    float currentHitpoints;
    Rigidbody rb;
    // Start is called before the first frame update

    // test for public slider
    public Slider enemyHealthSlider;
    void Start()
    {
        EntityHealth.SetMaxHitpoints(EntityHealth.hitPoints);
        //this is the start, determning the max hit points from the get go
        maxEmHitpoints = EntityHealth.hitPoints;

        //EntityHealth.SetMaxHitpoints()
        //int roundedMaxHit = Mathf.RoundToInt(maxEmHitpoints);
        //EntityHealth.SetMaxHitpoints(roundedMaxHit);

        Debug.Log("Enemy health: " + maxEmHitpoints);

        rb = GetComponent<Rigidbody>();
        if (rb != null )
        {
            Debug.LogWarning("Rigidbody component found on DumEnemCharacter.");
        }
        else
        {
            Debug.LogWarning("Rigidbody component not found on DumEnemCharacter GameObject.");
        }

    }

    public void TakeDamage(float damageAmount)
    {
        //currentHealth -= damageAmount; // Reduce current health by the damage amount
        int roundedDamage = Mathf.RoundToInt(damageAmount);
        EntityHealth.ApplyDamage(roundedDamage);

        Debug.LogWarning("After hit, enemy health is now: " + EntityHealth.hitPoints);
    }

        // Update is called once per frame
        void Update()
    {
        //UpdateEnemyHealthSlider();
        //UpdateHealthBar();
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float fillPercentage = (float)EntityHealth.hitPoints / (float)maxEmHitpoints;
        Debug.LogWarning(" enemy hitpoints: " + EntityHealth.hitPoints);
        Debug.LogWarning(" enemy maxEmHitpoints: " + maxEmHitpoints);
        Debug.LogWarning(" enemy fillPercentage: " + fillPercentage);

        enemyHealthSlider.value = fillPercentage;
        //enemyHealthSlider.maxValue = maxEmHitpoints * 100;
        //enemyHealthSlider.minValue = 0;

    }

    //private void UpdateHealthBar()
    //{
    //we are going to need a max and a current healthpoint
    //if (enemyHealthSlider != null)
    //{
    //so the maxhitpoints needs to be the number from the start, the entityhealthpoints can be whats current
    //so the enetitypoints change while the maxEmHitpoints stay at 50
    //int fillPercentage = EntityHealth.hitPoints / maxEmHitpoints;
    //float fillPercentage = (float)EntityHealth.hitPoints / (float)maxEmHitpoints;

    //Debug.LogWarning(" enemy hitpoints: " + EntityHealth.hitPoints);
    //Debug.LogWarning(" enemy maxEmHitpoints: " + maxEmHitpoints);
    //Debug.LogWarning(" enemy fillPercentage: " + fillPercentage);
    //float fillPcorrected = (float)fillPercentage;
    //Debug.LogWarning(" enemy fillPcorrected: " + fillPcorrected);
    //enemyHealthSlider.value = fillPcorrected;
    //enemyHealthSlider.value = fillPercentage;


    // Calculate the fill percentage based on current health and maximum health
    //float fillPercentage = (float)EntityHealth.hitPoints / (float)EntityHealth.maxHitpoints;

    // Update the value of the health bar slider
    //enemyHealthSlider.value = fillPercentage;
    //}
    //}

    //public void UpdateHealthbar(UnityEngine.UI.Slider healthbar)
    //{
    //    if (healthbar.maxValue != maxHitpoints)
    //    {
    //        healthbar.maxValue = maxHitpoints;
    //    }
    //    healthbar.value = hitPoints;
    //}


}
