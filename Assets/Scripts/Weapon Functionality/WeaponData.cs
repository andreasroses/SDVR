
using System.Linq;
using UnityEngine;
public enum FiringMode
{
    Single,
    Burst,
    Auto
}

public enum ShootType
{
    Hitscan,
    Projectile
}

public enum BulletSpreadType
{
    None, //No Spread
    Simple, //Uses Vector3 for Spread Pattern
}

/// <summary>
///This class is used to store the data for the weapon as well as some functionality
/// </summary>

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Data/New Weapon")]
public class WeaponData : ScriptableObject
{
    #region WEAPON VARIABLES

    [Header("Weapon Identification")]
    public string WeaponName; //Name of the weapon
    public int WeaponID; //Unique ID for the weapon

    [Header("Weapon Audio")]
    public AudioClip[] FireAudio;
    public AudioClip[] ReloadAudio;

    [Header("Weapon Stats")]
    public int AmmoCapacity; 
    public int MagazineSize;
    [Tooltip("Make sure this is used for burst fire only!")]public int BulletsPerShot;
    public float BurstInterval; //Time between each bullet within a burst shot
    public float FireRate;   
    public float ReloadTime;
    public float Range;
    public float ReloadSpeed; //The speed at which the weapon reloads
    public float RecoilRecoveryTime; //The time it takes for the recoil to recover
    public float MaxSpreadTime; 
    public FiringMode FiringType; //The firing mode of the weapon
    public ShootType ProjectileType; //The type of projectile the weapon fires
    public BulletSpreadType SpreadType; //The type of spread the weapon has

    [Header("Simple Spread")]
    public Vector3 SimpleSpread;

    public GameObject Bullet; //The projectile object the weapon fires

    [Header("Accesibility")]
    public bool IsAutotrigger; //If the player chooses to hold down the trigger regardless of firing mode
    #endregion

    #region BULLET SPREAD FUNCTIONS
    public Vector3 GetSpread(float ShootTime = 0)
    {
        Vector3 spread = new Vector3(0,0,0);

        if(SpreadType == BulletSpreadType.Simple)
        {
            spread = Vector3.Lerp(
                Vector3.zero,
                new Vector3(
                    Random.Range(-SimpleSpread.x, SimpleSpread.x),
                    Random.Range(-SimpleSpread.y, SimpleSpread.y),
                    Random.Range(-SimpleSpread.y, SimpleSpread.y)
                ),
                Mathf.Clamp01(ShootTime / MaxSpreadTime));
        }
        else if (SpreadType == BulletSpreadType.None)
        {
            return spread;
        }

        return spread;
    }
    #endregion

    #region AUDIO FUNCTIONS 
    //public AudioClip GetRandomFireAudio() => FireAudio[Random.Range(0, FireAudio.Length)];

    //public AudioClip GetRandomReloadAudio() => ReloadAudio[Random.Range(0, ReloadAudio.Length)];
    #endregion
}