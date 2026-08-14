using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

public static class SaveAndLoad
{
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
}
