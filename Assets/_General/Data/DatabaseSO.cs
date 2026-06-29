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

    public List<SkillCard> skillPrefabs = new();
    public List<Enemy> enemyPrefabs = new();
}
