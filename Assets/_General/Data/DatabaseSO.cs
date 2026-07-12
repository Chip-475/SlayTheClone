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

    [Header("Map Generation Settings")]
    public int minNodesPerLayer;
    public int maxNodesPerLayer;
    public int bossLayer;
    public int maxEliteBattles;
    public int maxEvents;
    public int maxShops;
    public int maxRests;
    public int maxShortcuts;
    [Space]
    public float nodeOffsetX;
    public float nodeOffsetY;
    [Space]

    [Header("Misc")]
    public List<SkillCard> allSkillPrefabs = new();
    public List<Enemy> allEnemyPrefabs = new();
    public List<RecipeSO> unlockedRecipes;

    public Settings settings;

    #region Utilities
    public IEnumerable<int> MaxNodeAmounts()
    {
        yield return maxEliteBattles;
        yield return maxEvents;
        yield return maxShops;
        yield return maxRests;
        yield return maxShortcuts;
    }
    #endregion

    #region Utilities
    [System.Serializable]
    public class Settings
    {
        public float masterVolume;
        public float sfxVolume;
        public float bgmVolume;
    }
    #endregion
}
