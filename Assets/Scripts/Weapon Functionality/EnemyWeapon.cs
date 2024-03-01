using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kitbashery.Gameplay;
using UnityEditor.PackageManager;

public class EnemyWeapon : WeaponBase
{
    private bool CanShoot = false;

    protected override void Update()
    {
        Tick(CanShoot);
    }

    

    public void SwitchShootingMode(){
        CanShoot = !CanShoot;
    }

}
    
