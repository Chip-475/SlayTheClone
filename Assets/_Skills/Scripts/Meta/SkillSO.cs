using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Base class for all skills
public abstract class SkillSO : ScriptableObject
{
    [System.Serializable]
    public enum AttackRange
    {
        Melee,
        Ranged
    }


    public Animation anim;
    [Space]
    [Header("Characteristics")]
    public int cost;
    public int numberOfTargets;
    [Space]
    public AttackRange range;
    public List<DamageTypeSO> damageTypes = new();
    [Space]
    [Header("Statistics")]
    public int atkMin;
    public int atkMax;
    [Space]
    public float healMin;
    public float healMax;
    [Space]
    public float shield;
    
    public abstract IEnumerator Execute(IBattleEntity caster, List<IBattleEntity> targets);
}
