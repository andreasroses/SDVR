using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AIAgentConfig : ScriptableObject
{
    public float maxTime = 1.0f;
    public float maxDistanceFromPlayer = 20.0f;
    public float minDistanceFromPlayer = 10.0f;

    public float stopDistance = 5.0f;
    public float speed = 1.0f;
    public LayerMask playerMask;
}
