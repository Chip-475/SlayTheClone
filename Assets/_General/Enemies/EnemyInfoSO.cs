using UnityEngine;
using System;
using System.Collections.Generic;

// Stores stat data for each enemy
[CreateAssetMenu(fileName = "Enemy Info", menuName = "Scriptable Objects/Stats/Enemy Info")]
public class EnemyInfoSO : ScriptableObject
{
    [Serializable]
    public struct Resistances
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
    [Serializable]
    public struct Drop
    {
        public ItemSO item;
        public int dropChance;
        public int minAmount;
        public int maxAmount;
    }

    [Header("Combat Stats")]
    public int hp;
    public int maxHp;
    public int atk;
    public int startingAp;
    [Tooltip("Points per second.")] public int speed;
    public int accuracy;
    public Resistances resistances;

    [Header("Misc Info")]
    public List<Drop> dropPool;
}