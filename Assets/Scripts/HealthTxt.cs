using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kitbashery.Gameplay;
using UnityEngine.UI;
using TMPro;
public class HealthTxt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthTxt;
    [SerializeField] private Health entityHealth;
    [SerializeField] private string entityName;

    void Start(){
        string currHealth = entityName + " Health: " + entityHealth.hitPoints.ToString();
        healthTxt.text = currHealth;
    }
    public void UpdateHealth(){
        string currHealth = entityName + " Health: " + entityHealth.hitPoints.ToString();
        healthTxt.text = currHealth;
    }
}
