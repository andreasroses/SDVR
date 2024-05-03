using System.Collections;
using UnityEngine;

public class EnemyWeapon : WeaponBase
{
    private bool CanShoot = false; //instead of using mouse input, this class uses a bool to switch from shooting to not shooting
    [SerializeField] float shootDelay = 3;
    protected override void Update(){
        if(CanShoot){
            if(currentAmmo <= 0){
                Reload();
            }
            Shoot();
        }
    }

    public void SwitchShootingMode(){ //switches shooting mode
        CanShoot = !CanShoot;
    }

    public bool ShootingMode(){
        return CanShoot;
    }
    public Transform GetMuzzle(){
        return Muzzle;
    }
}
    
