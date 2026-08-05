using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

[DefaultExecutionOrder(-100)]
public class Database : MonoBehaviour
{
    #region Declarations
    public static Database instance;

    // Skills
    public List<Skill> skills = new();
    public static Dictionary<string, Skill> skillsDB = new();
    public static List<string> unlockedSkills = new();
    public static List <string> equippedSkills = new();
    public List<string> tempEquipped = new();
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadSkills();

        equippedSkills.AddRange(tempEquipped);
    }
    #endregion

    #region Methods
    void LoadSkills()
    {
        foreach (var s in skills) { var skill = Instantiate(s); skillsDB.Add(skill.data.Id, skill); }
        foreach (var s in skillsDB.Where(s => s.Value.data.unlocked)) unlockedSkills.Add(s.Key);
    }
    #endregion

    #region Utilities
    public static Transform FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;

            Transform result = FindDeepChild(child, name);

            if (result != null)
                return result;
        }

        return null;
    }

    // Skills
    public static Skill GetSkill(string id) { return skillsDB[id]; }
    public static IEnumerable<KeyValuePair<string, Skill>> GetSkill(Func<KeyValuePair<string, Skill>, bool> condition) { return skillsDB.Where(condition); }
    #endregion
}
