using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class WeaponHolder : MonoBehaviour
{
    
    
    [Header("References")]
    [SerializeField] private Transform[] weapons;

    [Header("XR_Input")]
    //[SerializeField] private InputActionReference SwitchWeapon;
    //[SerializeField] private InputActionReference ShootWeapon;
    //[SerializeField] private InputActionReference ReloadWeapon;

    [SerializeField] private TextMeshProUGUI weaponStatusText;

    [Header("Settings")]
    [SerializeField] private float switchTime;

    private int selectedWeapon;
    private float timeSinceLastSwitch;
    private float switchWeaponValue;
    private float shootWeaponValue;

    private void Start()
    {

        SetWeapons();
        Select(selectedWeapon);

        timeSinceLastSwitch = 0f;
    }

    private void SetWeapons()
    {
        weapons = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            weapons[i] = transform.GetChild(i);

        //if (keys == null) keys = new KeyCode[weapons.Length];
    }

    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        //for (int i = 0; i < keys.Length; i++)
        //    if (Input.GetKeyDown(keys[i]) && timeSinceLastSwitch >= switchTime)
        //        selectedWeapon = i;

        ScrollSelect();
        //GetShootInput();
        //GetReloadInput();
        //GetSwapInput();
        if (previousSelectedWeapon != selectedWeapon) Select(selectedWeapon);

        timeSinceLastSwitch += Time.deltaTime;

        weaponStatusText = weapons[selectedWeapon].GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>();
        weaponStatusText.text = weapons[selectedWeapon].GetComponent<WeaponBase>().currentAmmo.ToString() + "/" + weapons[selectedWeapon].GetComponent<WeaponBase>().maxAmmo.ToString();

    }
    private void ScrollSelect()
    {
        // Detect scroll input to change weapons
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= weapons.Length - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = weapons.Length - 1;
            else
                selectedWeapon--;
        }

        //if (GetSwapInput())
        //{
        //    if (selectedWeapon >= weapons.Length - 1)
        //        selectedWeapon = 0;
        //    else
        //        selectedWeapon++;
        //}


    }

    //private bool GetSwapInput()
    //{
    //    if(SwitchWeapon.action.triggered)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //private void GetShootInput()
    //{
    //    shootWeaponValue = ShootWeapon.action.ReadValue<float>();
    //    if(shootWeaponValue > 0)
    //    {
    //        weapons[selectedWeapon].GetComponent<WeaponBase>().Shoot();
    //    }
    //}

    //private void GetReloadInput()
    //{
    //    if (ReloadWeapon.action.triggered)
    //    {
    //        weapons[selectedWeapon].GetComponent<WeaponBase>().Reload();
    //    }
    //}   

    private void Select(int weaponIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(i == weaponIndex);

        timeSinceLastSwitch = 0f;
    }





}
