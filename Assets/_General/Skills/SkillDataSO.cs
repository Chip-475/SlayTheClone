using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.Windows;

[CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable Objects/Skill Data")]
public class SkillDataSO : ScriptableObject
{
    public enum DamageTypes
    {
        Physical,
        Fire,
        Ice,
        Lightning,
        Arcane
    }
    public enum SkillType
    {
        Offensive,
        Defensive,
        Supportive
    }

    public string Id
    {
        get
        {
            return Regex.Replace(skillName.Trim(), @"\s+", "_").ToLower();
        }
        private set { return; }
    }
    public bool unlocked;

    public string skillName;
    [TextArea] public string description;

    public SkillType skillType;
    public int staminaCost;
    public int damagePercentage;
    public List<DamageTypes> damageTypes;
}
