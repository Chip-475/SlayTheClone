using UnityEngine;
using System.Collections.Generic;
using static SkillDataSO;

public class Skill : MonoBehaviour
{
    #region Declarations
    protected CombatManager Manager => CombatManager.instance;

    [Header("To Fill Out")]
    public SkillDataSO data;
    public Skill skill;
    [Space]

    [Header("Meta Data")]
    public string id;
    public bool unlocked;

    [Header("Data")]
    public string skillName;
    public SkillType skillType;

    [Header("Stats")]
    public int staminaCost;
    public int damagePercentage;
    public List<DamageTypes> damageTypes;
    #endregion

    public virtual void Awake()
    {
        id = data.Id;
        unlocked = data.unlocked;
        skillName = data.skillName;
        skillType = data.skillType;
        staminaCost = data.staminaCost;
        damagePercentage = data.damagePercentage;
        damageTypes = new List<DamageTypes>(data.damagePercentage);

        DontDestroyOnLoad(gameObject);
    }

    #region Methods
    public virtual void Effect(Enemy target) { print("Empty skill used."); }
    public virtual void Effect(Player target) { print("Empty skill used."); }

    public virtual void Select() { Manager.selectedSkill = skill; }
    public virtual void OnPointerEnter()
    {
        return;
    }
    public virtual void OnPointerExit()
    {
        return;
    }
    #endregion
}
