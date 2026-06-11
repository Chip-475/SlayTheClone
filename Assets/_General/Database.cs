using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =("Database"), menuName = ("Database"))]
public class Database : ScriptableObject
{
    // ONLY CREATE A SINGLE INSTANCE

    public List<CardSkill> skillPrefabs = new();
    public List<Enemy> enemyPrefabs = new();

    public List<DamageTypeSO> damageTypes = new();
}
