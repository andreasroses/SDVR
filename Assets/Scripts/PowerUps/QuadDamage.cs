using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuadDamage", menuName = "PowerUps/QuadDamage")]
public class QuadDamage : PowerUpData
{
    public override void ApplyPowerUp(Entity stats)
    {
        if(stats.Stats.DamageMultiplier != (stats.Stats.DamageMultiplier * StatBoost))
        {
            stats.Stats.DamageMultiplier *= StatBoost;
            return;
        }
    }

    public override void RemovePowerUp(Entity stats)
    {
        stats.Stats.DamageMultiplier /= StatBoost;
    }
}
