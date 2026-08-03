using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

[DefaultExecutionOrder(-10)]
public class Database : MonoBehaviour
{
    #region Declarations
    public static Database instance;

    // Skills
    public List<Skill> skills = new();
    public static Dictionary<string, Skill> skillsDB = new();
    public static List<string> unlockedSkills = new();
    public static List <string> equippedSkills = new();
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
    }
    #endregion

    #region Methods
    void LoadSkills()
    {
        foreach (var s in skills) skillsDB.Add(s.data.Id, s);
        foreach (var s in skillsDB.Where(s => s.Value.data.unlocked)) unlockedSkills.Add(s.Key);
    }
    #endregion

    #region Utilities
    // Skills
    public static Skill GetSkill(string id) { return skillsDB[id]; }
    public static IEnumerable<KeyValuePair<string, Skill>> GetSkill(Func<KeyValuePair<string, Skill>, bool> condition) { return skillsDB.Where(condition); }
    #endregion
}
