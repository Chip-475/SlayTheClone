using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

// Base class for all skills
public abstract class SkillSO : ScriptableObject
{
    [Serializable]
    public struct DamageTable
    {
        public float slash;
        public float pierce;
        public float blunt;
        public float fire;
        public float ice;
        public float magic;

        public readonly IEnumerable<float> Values()
        {
            yield return slash;
            yield return pierce;
            yield return blunt;
            yield return fire;
            yield return ice;
            yield return magic;
        }
    }

    public enum AttackRange
    {
        Melee,
        Ranged
    }
    public enum TargetingMode
    {
        Single,
        Nearest,
        All
    }


    public string id;
    public int amount;
    [Space]
    [Header("Characteristics")]
    public int numberOfTargets;
    [Space]
    public Animation anim;
    public AttackRange range;
    public TargetingMode targetingMode;
    public DamageTable damageTable;
    [Space]
    [Header("Stats")]
    public int cost;
    [TextArea] public string desc;
    [Space]
    public int atkMin;
    public int atkMax;
    [Space]
    public float healMin;
    public float healMax;
    [Space]
    public float shield;
    
    public abstract IEnumerator Effect(Enemy target);
}
