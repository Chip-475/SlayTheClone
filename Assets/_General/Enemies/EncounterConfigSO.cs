using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Encounter Config", menuName = "Scriptable Objects/Encounter Config")]
public class EncounterConfigSO : ScriptableObject
{
    public List<Enemy> enemies = new();
}
