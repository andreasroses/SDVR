using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kitbashery.Gameplay;

[AddComponentMenu("Kitbashery/Gameplay/Health")]
public class MockHealth : Health
{
    private void Start()
    {
        hitPoints = 100;
        Debug.Log("MockHealth: " + hitPoints);
    }
}
