using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpData data;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = data.sound;
    }



    private void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
        if (other.CompareTag("Player"))
        {
            ApplyPowerUp(other);
            
        }
    }

    private void ApplyPowerUp(Collider player)
    {
        var entity = player.GetComponent<Entity>();
        data.ApplyPowerUp(entity);
        if(data.isTimed)
        {
            StartCoroutine(RemovePowerUp(data.Duration, entity));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator RemovePowerUp(float duration, Entity entity)
    {
        yield return new WaitForSeconds(duration);
        data.RemovePowerUp(entity);
        Destroy(gameObject);
    }
}
