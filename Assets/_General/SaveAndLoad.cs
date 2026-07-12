using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SaveAndLoad : MonoBehaviour
{
    #region Declarations
    DatabaseSO Database => DB.instance.database;

    public static SaveAndLoad instance;

    string settingsPath;
    #endregion

    #region Unity Methods
    private void Awake()
    {
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
    }
    #endregion

    #region Settings
    [ContextMenu("Save")]
    public void SaveSettings()
    {
        DatabaseSO.Settings data = Database.settings;

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(settingsPath, json);
    }
    [ContextMenu("Load")]
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
}
