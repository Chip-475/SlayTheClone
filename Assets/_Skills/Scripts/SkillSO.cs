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


    public Animation anim;
    [Space]
    [Header("Characteristics")]
    public int cost;
    public int numberOfTargets;
    [Space]
    public AttackRange range;
    public TargetingMode targetingMode;
    public DamageTable damageTable;
    [Space]
    [Header("Statistics")]
    public int atkMin;
    public int atkMax;
    [Space]
    public float healMin;
    public float healMax;
    [Space]
    public float shield;
    
    public abstract IEnumerator Effect(Enemy target);
}
