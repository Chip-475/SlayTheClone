using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    public static Database instance;
    private void Start()
    {
        instance = this;
    }

    public List<CardSkill> skillPrefabs = new();
    public List<Enemy> enemyPrefabs = new();
}
