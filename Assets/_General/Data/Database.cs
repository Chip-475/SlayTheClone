using UnityEngine;
using System.Collections.Generic;
using System;

[DefaultExecutionOrder(-100)]
public class Database : MonoBehaviour
{
    public static Database instance;
    public static bool initialized = false;

    #region Skills
    public List<Skill> skills = new();
    public SkillVisual skillAnim;
    public static Dictionary<string, Skill> skillsDB = new();
    public static List<Skill> unlockedSkills = new();
    public static Dictionary<int, Skill> equippedSkills = new();
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        InitSkillsDB();
        InitUnlockedSkills();

        initialized = true;
    }
    #endregion

    #region Methods
    void InitSkillsDB() => skills.ForEach(s => skillsDB.Add(s.data.Id, Instantiate(s)));
    public static void InitUnlockedSkills() { foreach (var s in skillsDB) if (s.Value.data.unlocked == true) unlockedSkills.Add(s.Value); }
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
    public static Skill GetSkillById(string id) { return skillsDB[id]; }
    #endregion
}
