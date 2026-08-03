using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
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

    public void Awake()
    {
        id = data.Id;
        unlocked = data.unlocked;
        skillName = data.skillName;
        skillType = data.skillType;
        staminaCost = data.staminaCost;
        damagePercentage = data.damagePercentage;
        damageTypes = new List<DamageTypes>(data.damagePercentage);
    }

    #region Methods
    public virtual void Effect(Enemy target) { return; }
    public virtual void Effect(Player target) { return; }

    public virtual void Select() { Manager.selectedSkill = skill; }
    #endregion
}
