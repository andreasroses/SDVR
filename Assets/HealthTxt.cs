using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kitbashery.Gameplay;
using UnityEngine.UI;
using TMPro;
public class HealthTxt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthTxt;
    [SerializeField] private Health playerHealth;
    public void UpdateHealth(){
        string currHealth = "Health: " + playerHealth.hitPoints.ToString();
        healthTxt.text = currHealth;
    }
}
