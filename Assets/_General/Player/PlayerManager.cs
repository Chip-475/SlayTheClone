using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Declarations
    public static PlayerManager instance;
    public static Player player;

    [Header("Meta")]
    public string statsPath;
    public string inventoryPath;

    [Header("Data Packets")]
    public static PlayerStats stats = new();
    public static Inventory inventory = new();
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        inventoryPath = Path.Combine(Application.persistentDataPath, "inventory.json");
        statsPath = Path.Combine(Application.persistentDataPath, "playerStats.json");
    }
    private void Start()
    {
        SaveAndLoad.Load(ref stats, statsPath);
        SaveAndLoad.Load(ref inventory, inventoryPath);

        if (player == null) return;
        player.Health = stats.maxHp; // testing
    }
    private void OnApplicationQuit()
    {
        SaveAndLoad.Save(stats, statsPath);
        SaveAndLoad.Save(inventory, inventoryPath);
    }
    #endregion
}

[System.Serializable]
public class Inventory
{
    public int money;

    public List<Skill> ownedSkills;
    public List<ItemSO> items;
}
[System.Serializable]
public class PlayerStats
{
    public int hp;
    public int maxHp;
    public int atk;
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