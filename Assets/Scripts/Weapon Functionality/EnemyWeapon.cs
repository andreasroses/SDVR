
using UnityEngine;

public class EnemyWeapon : WeaponBase
{
    private bool CanShoot = false; //instead of using mouse input, this class uses a bool to switch from shooting to not shooting

    protected override void Update()
    {
        if(CanShoot){
            Shoot();
        }
        if(currentAmmo < 0){
            Reload();
        }
    }

    public void SwitchShootingMode(){ //switches shooting mode
        CanShoot = !CanShoot;
    }

    public Transform GetMuzzle(){
        return Muzzle;
    }

}
    
