using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneFloat : MonoBehaviour
{
    public float amplitude = 0.5f; // The amount of movement in the y-axis
    public float frequency = 1f; // The speed of movement

    private Vector3 startPos; // The starting position of the object

    private void Start()
    {
        startPos = transform.position; // Store the starting position
    }

    private void Update()
    {
        // Calculate the new position using a sine wave
        float newY = startPos.y + amplitude * Mathf.Sin(frequency * Time.time);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
