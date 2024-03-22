using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kitbashery.Gameplay;
using UnityEditor.PackageManager;
using TMPro;
using UnityEngine.UIElements;

public class WeaponBase : MonoBehaviour, IAmmoConsumable
{

    [SerializeField] protected WeaponData Data;
    [SerializeField] protected Transform Muzzle;
    [SerializeField] protected GameObject Model;
    [SerializeField] private TextMeshProUGUI AmmoText; //This is just for testing purposes, ideally we would have a HUD system to display ammo
    protected AudioSource WeaponAudio;
    protected float LastShootTime;
    protected float InitialClickTime;
    protected float StopShootingTime;
    protected bool LastFrameWantedToShoot;
    protected int currentAmmo;
    protected int maxAmmo;
    protected bool isShooting;
    protected bool keyPress;


    protected virtual void Start()
    {
        //AmmoText.GetComponentInChildren<TextMeshProUGUI>();
        WeaponAudio = GetComponent<AudioSource>();
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
                keyPress = Input.GetKey(KeyCode.Mouse0);
                break;
            default:
                keyPress = Input.GetKeyDown(KeyCode.Mouse0);
                break;
        }
        if (keyPress)
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(!isShooting && currentAmmo < Data.MagazineSize)
                Reload();
        }
        //AmmoText.text = currentAmmo + " / " + maxAmmo;
    }

    protected virtual void Shoot()
    {
        if (currentAmmo > 0)
        {
            
            if (Time.time > Data.FireRate + LastShootTime)
            {
                LastShootTime = Time.time;

                switch (Data.FiringType)
                {
                    case FiringMode.Burst:
                        StartCoroutine(FireBurst());
                        break;
                    default:
                        isShooting = true;
                        FireBullet();
                        isShooting = false;
                        break;
                }
                
            }

        }
    }

    private IEnumerator FireBurst()
    {
        isShooting = true;
        for (int i = 0; i < Data.BulletsPerShot; i++)
        {
            if(currentAmmo <= 0)
                break;
            FireBullet();

            if (i < Data.BulletsPerShot - 1)
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

        switch(Data.ProjectileType)
        {
            case ShootType.Projectile:
                ProjectileFire(ShootDirection);
                break;
            case ShootType.Hitscan:
                RaycastFire(ShootDirection);
                break;
        }
        WeaponAudio.clip = Data.GetRandomFireAudio();
        WeaponAudio.Play();
            
    }

    private void ProjectileFire(Vector3 ShootDir)
    {
        GameObject bullet = ObjectPools.Instance.GetPooledObject(Data.Bullet.name); //Grab a bullet from the object pool
        bullet.transform.position = Muzzle.position;
        bullet.transform.forward = ShootDir;
        bullet.SetActive(true); //Note, the bullet will be disabled after it hits or if it's life span is up
        currentAmmo--;
    }

    private void RaycastFire(Vector3 ShootDir)
    {
        RaycastHit hit;

        if (Physics.Raycast(Muzzle.position, ShootDir, out hit, Data.Range))
        {
            Debug.Log(hit.transform.name);

        }
        Debug.DrawRay(Muzzle.position, ShootDir * Data.Range, Color.red, 1f);

        currentAmmo--;
    }

    protected virtual void Reload()
    {
        if(maxAmmo <= 0)
            return;


        if (maxAmmo < Data.MagazineSize)
        {
            currentAmmo = maxAmmo;
            maxAmmo = 0;
        }
        else
        {
            maxAmmo -= Data.MagazineSize - currentAmmo;
            currentAmmo = Data.MagazineSize;    
        }
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

    public void RestoreAmmo(int amount)
    {
        throw new System.NotImplementedException();
    }
}
    
