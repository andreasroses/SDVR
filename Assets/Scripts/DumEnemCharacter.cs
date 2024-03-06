using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Kitbashery.Gameplay;
using System;
using System.Collections;
using UnityEngine.UI;

public class DumEnemCharacter : Entity
{
    float maxEmHitpoints;
    Rigidbody rb;
    // Start is called before the first frame update

    // test for public slider
    public Slider enemyHealthSlider;
    void Start()
    {
        maxEmHitpoints = EntityHealth.hitPoints;
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
        UpdateEnemyHealthSlider();
    }

    private void UpdateEnemyHealthSlider()
    {
        if (enemyHealthSlider != null)
        {
            EntityHealth.UpdateHealthbar(enemyHealthSlider);
        }
    }
}
