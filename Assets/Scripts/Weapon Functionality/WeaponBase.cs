using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kitbashery.Gameplay;
using UnityEditor.PackageManager;
using TMPro;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] protected WeaponData Data;
    [SerializeField] protected Transform Muzzle;
    [SerializeField] protected GameObject Model;
    [SerializeField] private TextMeshProUGUI AmmoText; //This is just for testing purposes, ideally we would have a HUD system to display ammo
    protected float LastShootTime;
    protected float InitialClickTime;
    protected float StopShootingTime;
    protected bool LastFrameWantedToShoot;
    protected int currentAmmo;
    protected int maxAmmo;

    protected void Start()
    {
        LastShootTime = Time.time;
        InitialClickTime = Time.time;
        StopShootingTime = Time.time;
        currentAmmo = Data.MagazineSize;
        maxAmmo = Data.AmmoCapacity;
    }

    protected virtual void Update()
    {
        Tick(Input.GetKey(KeyCode.Mouse0));
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        AmmoText.text = currentAmmo + " / " + maxAmmo;
    }

    protected virtual void Shoot()
    {
        //Code is meant for recoil recovery, ignore for now
        if (Time.time - LastShootTime - Data.FireRate > Time.deltaTime)
        {
            float lastDuration = Mathf.Clamp(0, (StopShootingTime - InitialClickTime), Data.MaxSpreadTime);

            float lerpTime = (Data.RecoilRecoveryTime - (Time.time - StopShootingTime)) / Data.RecoilRecoveryTime;

            InitialClickTime = Time.time - Mathf.Lerp(0, lastDuration, Mathf.Clamp01(lerpTime));
        }


        if (Time.time > Data.FireRate + LastShootTime)
        {
            LastShootTime = Time.time;
            
            Vector3 SpreadAmount = Data.GetSpread(Time.time - InitialClickTime);//Get the spread amount based on when we started shooting & the time we've been shooting
            Vector3 ShootDirection = Muzzle.transform.forward + SpreadAmount;

            GameObject bullet = ObjectPools.Instance.GetPooledObject(Data.Bullet.name); //Grab a bullet from the object pool
            bullet.transform.position = Muzzle.position;
            bullet.transform.forward = ShootDirection;
            bullet.SetActive(true); //Note, the bullet will be disabled after it hits or if it's life span is up
            currentAmmo--;


            // Draw a line from the muzzle to the hit point or the shoot direction
            RaycastHit hit;
            if (Physics.Raycast(Muzzle.position, ShootDirection, out hit))
            {
                Debug.DrawLine(Muzzle.position, hit.point, Color.red);
            }
            else
            {
                Debug.DrawRay(Muzzle.position, ShootDirection * 100f, Color.red, 4); 
            }
        }
    }

    protected virtual void Reload()
    {
        maxAmmo -= Data.MagazineSize - currentAmmo;
        currentAmmo = Data.MagazineSize;
        Debug.Log(Data.MagazineSize);
    }


    /// <summary>
    /// I made Tick() as a means of determining whether or not the player can shoot at all. 
    /// In enemy variants of this class, we can choose to make our own logic for this
    /// </summary>
 
    protected virtual void Tick(bool WantsToShoot)
    {
        if (WantsToShoot && currentAmmo > 0)
        {
            LastFrameWantedToShoot = true;
            Shoot();
        }
        else if (!WantsToShoot && LastFrameWantedToShoot)
        {
            StopShootingTime = Time.time;
            LastFrameWantedToShoot = false;
        }
    }

}
    
