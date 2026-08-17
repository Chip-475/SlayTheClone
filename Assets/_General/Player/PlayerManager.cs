using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static SaveAndLoad;

public class PlayerManager : MonoBehaviour
{
    #region Declarations
    public static PlayerManager instance;
    public static Player player;
    public static bool newFile;

    [Header("Data Packets")]
    public static PlayerStats P_Stats = new();
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        if(generalSaveFile.playerStats != null) P_Stats = generalSaveFile.playerStats;
        if (newFile) P_Stats = new();

        if (player != null) player.Health = P_Stats.maxHp; // testing
    }
    private void OnEnable()
    {
        FillSaveFile += () => generalSaveFile.playerStats = P_Stats;
    }
    private void OnDisable()
    {
        FillSaveFile -= () => generalSaveFile.playerStats = P_Stats;
    }
    #endregion
}