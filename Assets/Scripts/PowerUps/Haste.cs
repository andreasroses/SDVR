using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Haste", menuName = "PowerUps/Haste")]
public class Haste : PowerUpData
{
    public override void ApplyPowerUp(Entity stats)
    {
        if (stats.Stats.FireRateMultiplier != (stats.Stats.FireRateMultiplier * StatBoost))
        {
            stats.Stats.FireRateMultiplier *= StatBoost;
        }
    }

    public override void RemovePowerUp(Entity stats)
    {
        stats.Stats.FireRateMultiplier /= StatBoost;
    }
}
