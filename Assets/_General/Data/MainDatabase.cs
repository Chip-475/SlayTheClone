using System.Collections.Generic;
using UnityEngine;

public class MainDatabase : MonoBehaviour
{
    #region Declarations
    public static MainDatabase instance;

    public Settings settings = new();
    public Inventory inventory = new();

    public PlayerStats playerStats = new();
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
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
    #endregion

    #region Utilities
    public IEnumerable<int> MaxNodeAmounts()
    {
        yield return maxEliteBattles;
        yield return maxEvents;
        yield return maxShops;
        yield return maxRests;
        yield return maxShortcuts;
    }

    [System.Serializable]
    public class Settings
    {
        public float masterVolume;
        public float sfxVolume;
        public float bgmVolume;
    }
    [System.Serializable]
    public class PlayerStats
    {
        public int hp;
        public int maxHp;
        public int actionPointsSpeed;

        [Header("Misc")]
        public int timesDied;
        public int enemiesKilled;
        public int distanceTravelled;
        public int moneyEarned;
        public int cardsPlayed;
        public int totDamageDealt;
        public int highestDamageDealt;

        [Tooltip("ID of the enemy that killed the player.")]
        public string killer;
    }
    [System.Serializable]
    public class Inventory
    {
        public int money;

        public List<SkillCard> cards;
        public List<ItemSO> items;
    }
    #endregion
}
