using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public static event Action OnCombatStart;
    public static event Action OnEntityActing;
    public static event Action OnCardPlayed;

    public List<IBattleEntity> entitiesOnField;
    public List<IBattleEntity> actingEntities;
    [SerializeField] List<Transform> spawnPoints = new();
    [SerializeField] EncounterConfigSO encounterConfig;

    private void Start()
    {
        instance = this;

        CombatStart();
    }

    //
    void SpawnEnemies()
    {
        // Spawns each enemy at its assigned position in the entry
        for(int i = 0; i < encounterConfig.enemies.Count; i++)
        {
            if(encounterConfig.enemies[i] != null) Instantiate(encounterConfig.enemies[i], spawnPoints[i].position, Quaternion.identity);
        }
    }
    void GetEntities()
    {
        // Searches for all GameObjects with IBattleEntity and caches them
        var toReturn = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IBattleEntity>()
            .ToList();
        entitiesOnField = new List<IBattleEntity>(toReturn);
    }
    void StopActionBars()
    {
        foreach(var entity in entitiesOnField)
        {
            entity.StopActionBar();
        }
    }
    void StartActionBars()
    {
        foreach (var entity in entitiesOnField)
        {
            entity.StartActionBar();
        }
    }

    // Events
    public static void CombatStart()
    {
        OnCombatStart?.Invoke();
    }
    public static void EntityActing()
    {
        OnEntityActing?.Invoke();
    }
    public static void CardPlayed()
    {
        OnCardPlayed?.Invoke();
    }

    // Management
    private void OnEnable()
    {
        OnCombatStart += SpawnEnemies;
        OnCombatStart += GetEntities;

        OnEntityActing += StopActionBars;
    }
    private void OnDisable()
    {
        OnCombatStart -= SpawnEnemies;
        OnCombatStart -= GetEntities;

        OnEntityActing -= StopActionBars;
    }
}
