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

    string playerStatsPath;
    string settingsPath;
    string itemsPath;
    string skillsPath;
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
        playerStatsPath = Path.Combine(Application.persistentDataPath, "playerStats.json");
        settingsPath = Path.Combine(Application.persistentDataPath, "settings.json");
        itemsPath = Path.Combine(Application.persistentDataPath, "items.json");
        skillsPath = Path.Combine(Application.persistentDataPath, "skills.json");

        LoadPlayerStats();
        LoadSettings();
        LoadItems();
        LoadSkills();
    }
    private void OnApplicationQuit()
    {
        SavePlayerStats();
        SaveSettings();
        SaveItems();
        SaveSkills();
    }
    #endregion

    #region Player Stats
    [ContextMenu("Save Player Stats")]
    public void SavePlayerStats()
    {
        PlayerStatsSO data = Database.playerStats;

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(playerStatsPath, json);
    }
    [ContextMenu("Load Player Stats")]
    public void LoadPlayerStats()
    {
        if (File.Exists(playerStatsPath))
        {
            string json = File.ReadAllText(playerStatsPath);
            Database.playerStats = JsonConvert.DeserializeObject<PlayerStatsSO>(json);
        }
        else
        {
            Debug.Log("No stats save file found.");
        }
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

    #region Items
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
            Debug.Log("No items save file found.");
        }
    }
    #endregion

    #region Skills
    [ContextMenu("Save Skills")]
    public void SaveSkills()
    {
        List<SkillDatabase.SkillSaveData> skillData = new();
        foreach (var skill in ItemDatabase.instance.itemTable.Values)
        {
            skillData.Add
            (
                new SkillDatabase.SkillSaveData
                {
                    id = skill.id,
                    amount = skill.amount
                }
            );
        }

        string json = JsonConvert.SerializeObject(skillData, Formatting.Indented);
        File.WriteAllText(skillsPath, json);
    }
    [ContextMenu("Load Skills")]
    public void LoadSkills()
    {
        if (File.Exists(skillsPath))
        {
            string json = File.ReadAllText(skillsPath);
            List<SkillDatabase.SkillSaveData> skillData = JsonConvert.DeserializeObject<List<SkillDatabase.SkillSaveData>>(json);
            foreach (var skill in skillData)
            {
                SkillDatabase.GetItem(skill.id).amount = skill.amount;
            }
        }
        else
        {
            Debug.Log("No skill save file found.");
        }
    }
    #endregion
}
