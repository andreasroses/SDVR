using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ScriptableObject
{
    public List<Item> Items = new List<Item>();
    public int MaxItemSlots = 10;
    public int CurrentItems;
    

    public void AddItem(Item item)
    {
        if(CurrentItems < MaxItemSlots)
        {
            Items.Add(item);
            CurrentItems++;
        }
    }

    public void RemoveItem(Item item)
    {
        if(Items.Contains(item))
        {
            Items.Remove(item);
            CurrentItems--;
        }
    }

    public void UseItem(Item item)
    {
        item.Use();
    }
}
