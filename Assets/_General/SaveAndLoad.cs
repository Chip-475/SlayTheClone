using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public static class SaveAndLoad
{
    public static void Save<T>(T toSave, string pathfile)
    {
        string json = JsonConvert.SerializeObject(toSave, Formatting.Indented);
        File.WriteAllText(pathfile, json);
    }
    public static void Load<T>(ref T toLoadInto, string pathfile)
    {
        if (File.Exists(pathfile))
        {
            string json = File.ReadAllText(pathfile);
            toLoadInto = JsonConvert.DeserializeObject<T>(json);
        }
        else Debug.LogWarning("No save file found.");
    }
}
