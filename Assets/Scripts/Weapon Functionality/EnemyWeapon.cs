using System.Collections;
using UnityEngine;

public class EnemyWeapon : WeaponBase
{
    private bool CanShoot = false; //instead of using mouse input, this class uses a bool to switch from shooting to not shooting
    [SerializeField] float shootDelay = 3;
    protected override void Update(){
        return;
    }
    public void StartShooting(){
        Debug.Log("EnemyWeapon: Coroutine started");
        StartCoroutine(ShootingDelay());
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

    private IEnumerator ShootingDelay()
    {
        Debug.Log("Running coroutine");
        while(CanShoot){
            Debug.Log("Shooting");
            if(currentAmmo <= 0){
                Reload();
            }
            Shoot();
        }
        yield return new WaitForSeconds(shootDelay);
    }


}
    
