/*  AIAgentConfig
*   Scriptable object for managing values used for states.
*   
*   References code by TheKiwiCoder: https://youtu.be/1H9jrKyWKs0?si=PL0S4V7rFnJ5V1fc
*/
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

    public float rotationSpeed = 4.0f;

    public float droneRadius = 2f;
    public float droneSpeed = 2f;
    public float droneAngle = 0f;
}
