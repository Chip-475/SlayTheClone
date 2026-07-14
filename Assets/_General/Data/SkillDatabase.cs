using System.Collections.Generic;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{
    #region Declarations
    public static SkillDatabase instance;

    public Dictionary<string, SkillSO> skillTable = new();
    #endregion

    #region Unity Methods
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);

        foreach (var skill in Resources.LoadAll<SkillSO>("Skills"))
        {
            skillTable.Add(skill.id, skill);
        }
    }
    #endregion

    #region Methods
    public static SkillSO GetItem(string id)
    {
        if (instance.skillTable.TryGetValue(id, out SkillSO skill))
        {
            return skill;
        }
        else
        {
            Debug.Log("Skill not found.");
            return null;
        }
    }
    #endregion

    [System.Serializable]
    public class SkillSaveData
    {
        public string id;
        public int amount;
    }
}
