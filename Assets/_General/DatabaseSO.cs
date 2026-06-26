using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =("Database"), menuName = ("Database"))]
public class DatabaseSO : ScriptableObject
{
    // ONLY CREATE A SINGLE INSTANCE

    public List<SkillCard> skillPrefabs = new();
    public List<Enemy> enemyPrefabs = new();
}
