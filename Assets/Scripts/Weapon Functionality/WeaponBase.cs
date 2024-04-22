using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kitbashery.Gameplay;
using UnityEditor.PackageManager;
using TMPro;
using UnityEngine.UIElements;

public class WeaponBase : MonoBehaviour
{
    #region VALUES
    [SerializeField] public WeaponData Data;
    [SerializeField] protected Transform Muzzle;
    [SerializeField] protected GameObject Model;
    protected AudioSource WeaponAudio;
    protected float LastShootTime;

    public int currentAmmo;
    public int maxAmmo;
    protected bool isShooting;
    protected bool keyPress;
    #endregion

    protected virtual void Start()
    { 
        WeaponAudio = GetComponent<AudioSource>();
        LastShootTime = Time.time;
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
    }

    public void Shoot()
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
        //Get the spread amount based on when we started shooting & the time we've been shooting
        Vector3 SpreadAmount = Data.GetSpread();
        Vector3 ShootDirection = Muzzle.transform.forward + SpreadAmount;

        switch (Data.ProjectileType)
        {
            case ShootType.Projectile:
                ProjectileFire(ShootDirection);
                break;
            case ShootType.Hitscan:
                RaycastFire(ShootDirection);
                break;
        }
        //WeaponAudio.clip = Data.GetRandomFireAudio();
        //WeaponAudio.Play();

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

    public void Reload()
    {
        if (!isShooting && currentAmmo < Data.MagazineSize)
            if (maxAmmo <= 0)
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

    public void RestoreAmmo(int amount)
    {
      if(maxAmmo + amount > Data.AmmoCapacity)
        maxAmmo = Data.AmmoCapacity;
      else
        maxAmmo += amount;
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }

}

