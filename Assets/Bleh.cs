using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Bleh : MonoBehaviour
{
    private Volume volume;

    private void Start()
    {
        volume = GetComponent<Volume>();
        volume.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            volume.enabled = true;
            StartCoroutine(VignettePause(5f));
        }
    }

    IEnumerator VignettePause(float v)
    {
        yield return new WaitForSeconds(v);
        volume.enabled = false;
    }
}
