using Kitbashery.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : WeaponBase
{
    protected override void Update()
    {
        Tick(Input.GetKey(KeyCode.Mouse0));
    }

    protected override void Shoot()
    {

        Debug.Log("Enemy Shooting");
    }

    protected override void Tick(bool WantsToShoot)
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
