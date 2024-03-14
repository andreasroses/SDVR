using Kitbashery.Gameplay;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Entity : MonoBehaviour, IPowerUsable
{
  
    [SerializeField] protected EntityStats Stats;
    protected Health EntityHealth;
    protected Ability PowerUp;

    protected virtual void Awake()
    {
        EntityHealth = GetComponent<Health>();
        EntityHealth.hitPoints = Stats.MaxHealth; 
    }

}
