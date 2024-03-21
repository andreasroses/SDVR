using Kitbashery.Gameplay;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityStats", menuName = "Entity/EntityStats")]
public class EntityStats : ScriptableObject
{
    public int EntityID;
    public string EntityName;
    public int MaxHealth;
    public int MaxSpeed;
}