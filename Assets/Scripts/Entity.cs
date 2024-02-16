using Kitbashery.Gameplay;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Entity : MonoBehaviour
{
    protected Health EntityHealth;

    protected virtual void Awake()
    {
        EntityHealth = GetComponent<Health>();
    }
}
