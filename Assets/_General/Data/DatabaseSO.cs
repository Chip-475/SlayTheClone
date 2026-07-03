using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =("Database"), menuName = ("Database"))]
public class DatabaseSO : ScriptableObject
{
    // ONLY CREATE A SINGLE INSTANCE

    public PlayerStatsSO playerStats;
    public Inventory inventory;
    [Space]
    public int nStartingCards;
    public int nCardsAtTurnStart;

    [Header("Misc")]
    public List<SkillCard> allSkillPrefabs = new();
    public List<Enemy> allEnemyPrefabs = new();
}
