using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using static SaveAndLoad;

[DefaultExecutionOrder(-100)]
public class Database : MonoBehaviour
{
    public static Database instance;
    public static bool initialized = false;
    public static bool newFile;
    public static bool bossNodeClicked;
    public static bool runEnded;

    public static Loadout loadout = new();

    #region Skills
    public static SkillData skillData = new();

    public List<Skill> skills = new();
    public static Dictionary<int, Skill> skillsDB = new();
    public static Dictionary<int, Skill> equippedSkills = new();

    public static List<int> AllUnlockedSkills => FindSkills(s => s.data.unlocked);
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        InitSkills();
    }
    private void Start()
    {
        skillData = generalSaveFile.skillData;
        foreach (var item in skillData.equippedSkills) equippedSkills[item.Key] = GetSkillById(item.Value);

        initialized = true;
    }
    private void Update()
    {
        if (newFile)
        {
            PlayerManager.newFile = true;
            MapManager.newFile = true;

            newFile = false;
        }
    }
    private void OnEnable()
    {
        FillSaveFile += () => generalSaveFile.skillData = skillData;   
    }
    private void OnDisable()
    {
        FillSaveFile -= () => generalSaveFile.skillData = skillData;
    }
    #endregion

    #region Methods
    void InitSkills()
    {
        if(initialized) return;

        // skillsDB
        skills.ForEach(s => s.data.id = skills.IndexOf(s) + 1);
        skills.ForEach(s => skillsDB.Add(s.data.id, Instantiate(s)));

        // equippedSkills
        for (int i = 0; i < 6; i++) equippedSkills.Add(i, null);
    }
    public static List<int> FindSkills(Func<Skill, bool> condition)
    {
        List<int> skillIndexes = new();

        foreach(var pair in skillsDB)
        {
            if(condition(pair.Value)) skillIndexes.Add(pair.Key);
        }

        return skillIndexes;
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
    public static Skill GetSkillById(int id) { return skillsDB[id]; }
    #endregion

    #region Misc
    public class Loadout
    {
        public Dictionary<int, int> equippedSkills = new();
    }
    #endregion
}
