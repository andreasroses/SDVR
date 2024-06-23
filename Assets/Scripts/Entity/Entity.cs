using Kitbashery.Gameplay;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Entity : MonoBehaviour
{
  
    public EntityStats Stats;
    [HideInInspector]public Health EntityHealth;

    protected virtual void Awake()
    {
        EntityHealth = GetComponent<Health>();
        EntityHealth.hitPoints = Stats.MaxHealth; 
    }

}
