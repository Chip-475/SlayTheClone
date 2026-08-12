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
    #endregion

    public virtual void Awake()
    {
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
