using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
using UnityEditor.Rendering;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;
using TMPro;

public class ExampleSavingLoading : MonoBehaviour
{
    private IDataService DataService = new JsonDataService();
    private bool EncryptionEnabled = false;
    public static bool loadedData = false, isSaving = false;
    private string saveSlotString = "/player-data-save-1.json";

    // *** Temporary ***
    public TextMeshProUGUI Label, sdText1,sdText2; 
    

    /* 
        The JsonDataService currently saves to {User}/AppData/LocalLow/{CompanyName}/SDVR/
    */


    public void LoadData(int saveSlot) 
    { // loads the data from file when opening the save file
        saveSlotString = $"/player-data-save-{saveSlot}.json";

        if(File.Exists(Application.persistentDataPath + PlayerPrefs.GetString("savePath", saveSlotString))){
            DeserializeJson();
        }else{
            // if no file then we will want to make save files
            Debug.Log("No Save file, making one");
            using FileStream stream = File.Create(Application.persistentDataPath + PlayerPrefs.GetString("savePath", saveSlotString));
            stream.Close();
        }
    }

    public void SaveData(int saveSlot){
        
        saveSlotString = $"/player-data-save-{saveSlot}.json";
        StartCoroutine(SavePlayerData());
    }

    public void SerializeJson(){ // will serialize and save the data
        isSaving = true;
        long startTime = System.DateTime.Now.Ticks, SaveTime;
        PlayerData playerData = new PlayerData();
        if (DataService.SaveData(PlayerPrefs.GetString("savePath", saveSlotString), playerData, EncryptionEnabled))
        {
            SaveTime = System.DateTime.Now.Ticks - startTime;
            Debug.Log($"Save time took: {(SaveTime / 10000f):N4}ms");
        }
        else
        {
            Debug.LogError("Could not save file!");
        }
        isSaving = false;
    }

    public void DeserializeJson(){ // will deserialize and load the data
        long startTime = System.DateTime.Now.Ticks;
        try
        {
            PlayerData data = DataService.LoadData<PlayerData>(PlayerPrefs.GetString("savePath", saveSlotString), EncryptionEnabled);
            long LoadTime = System.DateTime.Now.Ticks - startTime;
            Debug.Log($"Loaded Data From File, Load took {(LoadTime / 10000f):N4}ms");
            if(data == null){
                    
            }else{
                StartCoroutine(LoadPlayerData(data));
            }
            
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to read from file data to: {e.Message} {e.StackTrace}");
        }
    }

    IEnumerator SavePlayerData()
    {
        SerializeJson();
        
        while(isSaving){
            yield return null;
        }

        yield return null;
    }


    IEnumerator LoadPlayerData(PlayerData data)
    { 
        yield return new WaitForSeconds(.1f);

        // Load data here from the 'data' variable
        Label.text = "Loaded";
        sdText1.text = data.exampleData_1.ToString();
        sdText2.text = data.exampleData_2.ToString();

        // Once data is loaded, set to true, this is temp as it will always default to true
        loadedData = true;

        while(!loadedData)
        {
            yield return new WaitForSeconds(.1f);
        }

        yield return null;
    }

    
}
