using UnityEngine;
using System;
using System.Collections.Generic;

// Stores stat data for every enemy
[CreateAssetMenu(fileName = "Enemy Stats", menuName = "Scriptable Objects/Stats/Enemy Stats")]
public class EnemyStatsSO : ScriptableObject
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

    public int hp;
    public int maxHp;
    public int actionPointsSpeed;
    public Resistances resistances;
}