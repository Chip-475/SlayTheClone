using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Database")]
public class DatabaseSO : ScriptableObject
{
    // ONLY CREATE A SINGLE INSTANCE

    public PlayerStatsSO playerStats;
    public Inventory inventory;
    [Space]
    public int nStartingCards;
    public int nCardsAtTurnStart;
    [Space]
    public int minNodesPerLayer;
    public int maxNodesPerLayer;
    public int bossLayer;
    public int maxEliteBattles;
    [Space]
    public float nodeOffsetX;
    public float nodeOffsetY;

    [Header("Misc")]
    public List<SkillCard> allSkillPrefabs = new();
    public List<Enemy> allEnemyPrefabs = new();
    public List<RecipeSO> unlockedRecipes;
}
