using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class AmmoUI : MonoBehaviour
{
  
    [SerializeField] public GameObject Weapon;
    private WeaponBase weaponBase;

    [SerializeField] TextMeshProUGUI CurrentAmmo;
    [SerializeField] TextMeshProUGUI MaxAmmo;

    
    void Start() {
    
        weaponBase = Weapon.GetComponent<WeaponBase>();
    }

        void Update()
    {
        if (weaponBase != null && CurrentAmmo != null && MaxAmmo != null)
        {
            CurrentAmmo.text = "" + weaponBase.GetCurrentAmmo().ToString();
            MaxAmmo.text = "" + weaponBase.GetMaxAmmo().ToString();
        }

    }
}
