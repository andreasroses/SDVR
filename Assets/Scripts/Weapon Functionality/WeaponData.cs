using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
public enum FiringMode
{
    Single,
    Burst,
    Auto
}

public enum Projectile
{
    Hitscan,
    Projectile
}   

public enum BulletSpreadType
{
    None, //No Spread
    Simple, //Uses Vector3 for Spread Pattern
    TextureBased //Used B/W Texture to determine spread pattern
}

/// <summary>
///This class is used to store the data for the weapon as well as some functionality
/// </summary>

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Data/New Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Identification")]
    public string WeaponName; //Name of the weapon
    public int WeaponID; //Unique ID for the weapon

    [Header("Weapon Stats")]
    public int AmmoCapacity; 
    public int MagazineSize; 
    public float FireRate;   
    public float ReloadTime;
    public float Range; 
    public float RecoilRecoveryTime; //The time it takes for the recoil to recover
    public float MaxSpreadTime; 
    public FiringMode FiringType; //The firing mode of the weapon
    public Projectile ProjectileType; //The type of projectile the weapon fires
    public BulletSpreadType SpreadType; //The type of spread the weapon has

    [Header("Simple Spread")]
    public Vector3 SimpleSpread;

    [Header("Texture Based Spread")]
    [Range(0.001f, 5f)]
    public float SpreadScale = 0.1f; //The scale of the spread texture
    public Texture2D SpreadTexture; //The texture used to determine the spread pattern
    public GameObject Bullet; //The projectile object the weapon fires

    [Header("Accesibility")]
    public bool IsAutotrigger; //If the player chooses to hold down the trigger regardless of firing mode


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
        else if (SpreadType == BulletSpreadType.TextureBased)
        {
            spread = GetTextureDirection(ShootTime);
            spread *= SpreadScale;
        }
        else if (SpreadType == BulletSpreadType.None)
        {
            return spread;
        }

        return spread;
    }


    /// <summary>
    /// A bit of complicated math here, but it's used to determine the spread pattern based on the texture
    /// We get an increasingly larger sample size from a texture, and then randomly select the white pixel within it
    /// https://www.youtube.com/watch?v=yqlFrLGeRaQ <- this is the video we used to learn how to get the texture data
    /// </summary>
    private Vector2 GetTextureDirection(float ShootTime)
    {
        Vector2 halfsize = new Vector2(
                       SpreadTexture.width /2f, SpreadTexture.height /2f);
        int halfSquareExtents = Mathf.CeilToInt(
            Mathf.Lerp(1, halfsize.x, Mathf.Clamp01(ShootTime / MaxSpreadTime)));

        int minX = Mathf.FloorToInt(halfsize.x - halfSquareExtents);
        int minY = Mathf.FloorToInt(halfsize.y - halfSquareExtents);

        Color[] sampleColors = SpreadTexture.GetPixels(
                       minX, minY, halfSquareExtents * 2, halfSquareExtents * 2);

        float[] colorsAsGrey = System.Array.ConvertAll(sampleColors, (c) => c.grayscale);
        float totalGreyValue = colorsAsGrey.Sum();

        float grey = Random.Range(0, totalGreyValue);
        int i = 0;
        for(; i < colorsAsGrey.Length; i++)
        {
            grey -= colorsAsGrey[i];
            if (grey <= 0)
            {
                break;
            }
        }   

        int x = i % (halfSquareExtents * 2);
        int y = i / (halfSquareExtents * 2);

        Vector2 targetPosition = new Vector2(x, y);
        Vector2 direction = (targetPosition - halfsize) / halfsize.x;

        return direction;
    }
    #endregion

    

}

