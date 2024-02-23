using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [Header("Item Information")]
    public string Name;
    public string Description;
    public Sprite Icon;

    public virtual void Use() { }
    public virtual void AbilityIncrease (int amount) { }
    public virtual void AbilityDecrease (int amount) { }
    public virtual void ObjectSpawn () { }
}

