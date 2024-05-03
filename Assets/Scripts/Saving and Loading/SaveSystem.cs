// using System;
// using UnityEngine;
// using Input = UnityEngine.Input;

// [System.Serializable]
// public class WeaponAmmoData
// {
//     public int currentAmmo;
// }


// public class PlayerData
// {
//     public int health;
//     public Vector3 position;
//     public string weaponAmmoData; // Serialized JSON string for weapon ammo data
// }

// public class SaveSystem : MonoBehaviour
// {
//     PlayerData playerData;
//     WeaponAmmoData weaponAmmoData;
//     string saveFilePath;

//     void Start()
//     {
//         playerData = new PlayerData();
//         weaponAmmoData = new WeaponAmmoData();
//         saveFilePath = Application.persistentDataPath + "/PlayerData.json";
//         NewGame();
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.J))
//         {
//             SaveGame();
//         }
//         else if (Input.GetKeyDown(KeyCode.K))
//         {
//             LoadGame();
//         }
//         else if (Input.GetKeyDown(KeyCode.L))
//         {
//             DeleteSaveFile();
//         }
//         else if (Input.GetKeyDown(KeyCode.M))
//         {
//             NewGame();
//         }
//         else if (Input.GetKeyDown(KeyCode.I))
//         {
//             ChangeData();
//         }
//     }

//     public void SaveGame()
//     {
//         playerData.position = FindObjectOfType<PlayerCharacter>().PlayerPosition;
//         weaponAmmoData.currentAmmo = FindObjectOfType<WeaponBase>().GetCurrentAmmo();
//         playerData.weaponAmmoData = JsonUtility.ToJson(weaponAmmoData);
//         string savePlayerData = JsonUtility.ToJson(playerData);
//         System.IO.File.WriteAllText(saveFilePath, savePlayerData);
//         Debug.Log("Save file created at: " + saveFilePath);
//     }

//     public void LoadGame()
//     {
//         if (System.IO.File.Exists(saveFilePath))
//         {
//             string loadPlayerData = System.IO.File.ReadAllText(saveFilePath);
//             playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);
//             Debug.Log("Load game complete! \nPlayer health: " + playerData.health + ", Player Position: (x = "
//                 + playerData.position.x + ", y = " + playerData.position.y + ", z = " + playerData.position.z
//                 + "weapondata: " + playerData.weaponAmmoData + ")");
//             FindObjectOfType<PlayerCharacter>().SetPlayerPosition(playerData.position);
//             int ammoResult = Int32.Parse(playerData.weaponAmmoData);
//             FindAnyObjectByType<WeaponBase>().SetCurrentAmmo(ammoResult);
//         }
//         else
//         {
//             Debug.Log("There are no save files to load!");
//         }
//     }

//     public void DeleteSaveFile()
//     {
//         if (System.IO.File.Exists(saveFilePath))
//         {
//             System.IO.File.Delete(saveFilePath);
//             Debug.Log("Save file deleted!");
//         }
//         else
//         {
//             Debug.Log("There is nothing to delete!");
//         }
//     }

//     public void NewGame()
//     {
//         playerData.health = 100;
//         playerData.position = Vector3.zero;
//         Debug.Log("New game! \nPlayer health: " + playerData.health + ", Player Position: (x = "
//             + playerData.position.x + ", y = " + playerData.position.y + ", z = " + playerData.position.z + ")");
//     }

//     public void ChangeData()
//     { }
// }
