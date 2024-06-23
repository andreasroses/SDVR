using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponInput : MonoBehaviour
{

    [Header("Controller References")]
    [SerializeField] private InputActionReference ShootWeapon;
    [SerializeField] private InputActionReference ReloadWeapon;

    private WeaponBase weapon;

    private void Start()
    {
        weapon = GetComponent<WeaponBase>();
    }


    private void Update()
    {
        if (ReloadWeapon.action.triggered)
        {
            weapon.Reload();
        }

        if (ShootWeapon.action.triggered)
        {
            weapon.Shoot();
        }

    }

}
