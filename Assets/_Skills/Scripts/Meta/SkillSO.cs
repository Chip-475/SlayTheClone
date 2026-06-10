using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Base class for all skills
public abstract class SkillSO : ScriptableObject
{
    [System.Serializable]
    public struct AttackType
    {
        public bool Slash;
        public bool Pierce;
        public bool Blunt;

        public bool Magic;
        public bool Fire;
        public bool Ice;
    }
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
    public AttackRange range;
    public AttackType type;
    [Space]
    [Header("Statistics")]
    public int atkMin;
    public int atkMax;
    public float healMin;
    public float healMax;
    public float shield;
    
    public abstract IEnumerator PlayCard(IBattleEntity caster,List<IBattleEntity> targets);
}
