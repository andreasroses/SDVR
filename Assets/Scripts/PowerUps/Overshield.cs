using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Overshield", menuName = "PowerUps/Overshield")]
public class Overshield : PowerUpData
{
    public override void ApplyPowerUp(Entity stats)
    {
        if (stats.Stats.MaxHealth != (stats.Stats.MaxHealth * StatBoost))
        {
            stats.EntityHealth.hitPoints = stats.Stats.MaxHealth * StatBoost;
        }
    }

    public override void RemovePowerUp(Entity stats)
    {
        if(stats.EntityHealth.hitPoints > stats.Stats.MaxHealth)
            stats.EntityHealth.hitPoints = stats.Stats.MaxHealth;
        else
            stats.EntityHealth.hitPoints = stats.EntityHealth.hitPoints;
    }
}
