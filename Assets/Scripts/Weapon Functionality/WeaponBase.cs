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
    protected bool isShooting;

    protected virtual void Start()
    {
        AmmoText.GetComponentInChildren<TextMeshProUGUI>();
        LastShootTime = Time.time;
        InitialClickTime = Time.time;
        StopShootingTime = Time.time;
        currentAmmo = Data.MagazineSize;
        maxAmmo = Data.AmmoCapacity;
        isShooting = false;
    }

    protected virtual void Update()
    {
        switch(Data.FiringType)
        {
            case FiringMode.Auto:
                isShooting = Input.GetKey(KeyCode.Mouse0);
                break;
            default:
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
                break;
        }
        if (isShooting)
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        AmmoText.text = currentAmmo + " / " + maxAmmo;
    }

    protected virtual void Shoot()
    {
        Debug.Log("Shoot(): currentAmmo = " + currentAmmo);
        if (currentAmmo > 0)
        {
            Debug.Log("Shoot(): currentAmmo > 0");
            if (Time.time > Data.FireRate + LastShootTime)
            {
                LastShootTime = Time.time;

                switch (Data.FiringType)
                {
                    case FiringMode.Burst:
                        Debug.Log("Made it to B");
                        StartCoroutine(FireBurst());
                        break;
                    default:
                    Debug.Log("WeaponBase: Shooting");
                        FireBullet();
                        break;
                }
                
            }

        }
    }

    //WARNING: Burst fire is broken, do not use
    private IEnumerator FireBurst()
    {
        isShooting = true;
        for (int i = 0; i < Data.BurstCount; i++)
        {
            if(currentAmmo <= 0)
                break;
            FireBullet();

            if (i < Data.BurstCount - 1)
            {
                yield return new WaitForSeconds(Data.BurstInterval);
            }
        }
        isShooting = false;
    }

    private void FireBullet()
    {
        
        //Code is meant for recoil recovery, ignore for now
        //if (Time.time - LastShootTime - Data.FireRate > Time.deltaTime)
        //{
        //    float lastDuration = Mathf.Clamp(0, (StopShootingTime - InitialClickTime), Data.MaxSpreadTime);

        //    float lerpTime = (Data.RecoilRecoveryTime - (Time.time - StopShootingTime)) / Data.RecoilRecoveryTime;

        //    InitialClickTime = Time.time - Mathf.Lerp(0, lastDuration, Mathf.Clamp01(lerpTime));
        //}

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


    protected virtual void Reload()
    {
        maxAmmo -= Data.MagazineSize - currentAmmo;
        currentAmmo = Data.MagazineSize;
    }


    /// <summary>
    /// I made Tick() as a means of determining how the weapon model's recoil will function. 
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
    
