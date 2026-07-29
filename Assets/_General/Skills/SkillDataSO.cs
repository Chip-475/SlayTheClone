using UnityEngine;
using System.Collections.Generic;

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
        Defensive
    }

    public int id;
    public bool unlocked;

    public string skillName;
    [TextArea] public string description;

    public SkillType skillType;
    public int damagePercentage;
    public List<DamageTypes> damageTypes;
}
