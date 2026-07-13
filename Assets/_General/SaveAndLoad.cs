using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

[DefaultExecutionOrder(-1000)]
public class SaveAndLoad : MonoBehaviour
{
    #region Declarations
    DatabaseSO Database => DB.instance.database;

    public static SaveAndLoad instance;

    string settingsPath;
    string itemsPath;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        settingsPath = Path.Combine(Application.persistentDataPath, "settings.json");
        itemsPath = Path.Combine(Application.persistentDataPath, "items.json");

        LoadSettings();
        LoadItems();
    }
    private void OnApplicationQuit()
    {
        SaveSettings();
        SaveItems();
    }
    #endregion

    #region Settings
    [ContextMenu("Save Settings")]
    public void SaveSettings()
    {
        DatabaseSO.Settings data = Database.settings;

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(settingsPath, json);
    }
    [ContextMenu("Load Settings")]
    public void LoadSettings()
    {
        if(File.Exists(settingsPath))
        {
            string json = File.ReadAllText(settingsPath);
            Database.settings = JsonConvert.DeserializeObject<DatabaseSO.Settings>(json);
        }
        else
        {
            Debug.Log("No settings save file found.");
        }
    }
    #endregion

    #region Inventory
    [ContextMenu("Save Items")]
    public void SaveItems()
    {
        List<ItemDatabase.ItemSaveData> itemData = new();
        foreach (var item in ItemDatabase.instance.itemTable.Values)
        {
            itemData.Add
            (
                new ItemDatabase.ItemSaveData
                {
                    id = item.id,
                    amount = item.amount
                }
            );
        }

        string json = JsonConvert.SerializeObject(itemData, Formatting.Indented);
        File.WriteAllText(itemsPath, json);
    }
    [ContextMenu("Load Items")]
    public void LoadItems()
    {
        if (File.Exists(itemsPath))
        {
            string json = File.ReadAllText(itemsPath);
            List<ItemDatabase.ItemSaveData> itemData = JsonConvert.DeserializeObject<List<ItemDatabase.ItemSaveData>>(json);
            foreach (var item in itemData)
            {
                ItemDatabase.GetItem(item.id).amount = item.amount;
            }
        }
        else
        {
            Debug.Log("No inventory save file found.");
        }
    }
    #endregion
}
