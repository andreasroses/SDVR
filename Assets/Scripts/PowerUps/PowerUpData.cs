using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpData : ScriptableObject
{
    public int StatBoost;
    public float Duration;
    public AudioClip sound;
    public bool isTimed;
    public virtual void ApplyPowerUp(Entity stats)
    {
        return;
    }

    public virtual void RemovePowerUp(Entity stats)
    {
        return;
    }
}
