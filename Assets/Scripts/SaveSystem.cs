using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem
{


    private static SaveData saveData = new SaveData();

    [System.Serializable]
    public struct SaveData
    {
        public PlayerSaveData PlayerData;
        public InventorySaveData InventoryData;
    }
    public static string SaveFileName()
    {
        string saveFile = Path.Combine(Application.persistentDataPath, "save.save");
        return saveFile;
    }

    public static void Save()
    {
        HandleSaveData();
        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(saveData, true));
    }
    private static void HandleSaveData()
    {
        GameManager.Instance.Player.Save(ref saveData.PlayerData);
        GameManager.Instance.inventory.Save(ref saveData.InventoryData);
    }
    public static void Load()
    {
        if (!File.Exists(SaveFileName()))
        {
            Debug.LogError("Save file does not exist!");
            return; 
        }
        string saveContent = File.ReadAllText(SaveFileName());
        saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();
    }
    private static void HandleLoadData()
    {
        GameManager.Instance.Player.Load(saveData.PlayerData);
        GameManager.Instance.inventory.Load(saveData.InventoryData);
    }
}
