using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kitbashery.Gameplay;
using UnityEditor.PackageManager;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] protected WeaponData Data;
    [SerializeField] protected Transform Muzzle;
    [SerializeField] protected GameObject Model;

    private float LastShootTime;
    private float InitialClickTime;
    private float StopShootingTime;
    private bool LastFrameWantedToShoot;


    private void Update()
    {
        Tick(Input.GetKey(KeyCode.Mouse0));
    }

    protected void Shoot()
    {

        if (Time.time - LastShootTime - Data.FireRate > Time.deltaTime)
        {
            float lastDuration = Mathf.Clamp(0, (StopShootingTime - InitialClickTime), Data.MaxSpreadTime);

            float lerpTime = (Data.RecoilRecoveryTime - (Time.time - StopShootingTime)) / Data.RecoilRecoveryTime;

            InitialClickTime = Time.time - Mathf.Lerp(0, lastDuration, Mathf.Clamp01(lerpTime));
        }


        if (Time.time > Data.FireRate + LastShootTime)
        {
            LastShootTime = Time.time;
            Vector3 SpreadAmount = Data.GetSpread(Time.time - InitialClickTime);
            Vector3 ShootDirection = Muzzle.transform.forward + SpreadAmount;

            GameObject bullet = ObjectPools.Instance.GetPooledObject(Data.Bullet.name);
            bullet.transform.position = Muzzle.position;
            bullet.transform.forward = ShootDirection;
            bullet.SetActive(true);



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

    protected void Tick(bool WantsToShoot)
    {

        if (WantsToShoot)
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
    
