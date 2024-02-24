using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Variables")]
    public int totalHealth = 100;
    public int currentHealth;

    [Header("Components")]
    public Slider healthSlider;

    void Start()
    {
        currentHealth = totalHealth;

        healthSlider.maxValue = totalHealth;
        healthSlider.value = currentHealth;
    }

    private void Update()
    {
        healthSlider.value = currentHealth;

        if (currentHealth < 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    void Die()
    {
        gameObject.SetActive(false);
    }


}

public interface IDamageable
{
    void TakeDamage(int damage);
}
