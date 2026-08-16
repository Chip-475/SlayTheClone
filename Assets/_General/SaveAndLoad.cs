using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

[DefaultExecutionOrder(-500)]
public class SaveAndLoad : MonoBehaviour
{
    public static SaveAndLoad instance;

    public static SaveFile generalSaveFile = new();
    public string generalSaveFile_Path;
    public static event Action FillSaveFile;

    #region Unity Methods
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        generalSaveFile_Path = Path.Combine(Application.persistentDataPath, "generalSave.json");
        LoadAll();
    }
    private void OnApplicationQuit()
    {
        SaveAll();
    }
    #endregion

    public static void SaveAll()
    {
        FillSaveFile?.Invoke();
        string json = JsonConvert.SerializeObject(generalSaveFile, Formatting.Indented);
        File.WriteAllText(instance.generalSaveFile_Path, json);
    }
    public static void LoadAll()
    {
        if (!File.Exists(instance.generalSaveFile_Path)) throw new FileNotFoundException("Save file not found.", instance.generalSaveFile_Path);

        string json = File.ReadAllText(instance.generalSaveFile_Path);
        generalSaveFile = JsonConvert.DeserializeObject<SaveFile>(json);
    }

    public static void Save<T>(T toSave, string pathfile)
    {
        string json = JsonConvert.SerializeObject(toSave, Formatting.Indented);
        File.WriteAllText(pathfile, json);
    }
    public static T Load<T>(string pathfile)
    {
        if (!File.Exists(pathfile)) throw new FileNotFoundException("Save file not found.", pathfile);

        string json = File.ReadAllText(pathfile);
        return JsonConvert.DeserializeObject<T>(json);
    }

    public class SaveFile
    {
        public PlayerStats playerStats;
        public SkillData skillData = new();
        public Dictionary<int, List<Node.SaveData>> map = new();
    }
}
