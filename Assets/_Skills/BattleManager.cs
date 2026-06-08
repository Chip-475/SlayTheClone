using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public static event Action OnCombatStart;
    public static event Action OnActionStart;
    public static event Action OnCardPlayed;

    public List<IBattleEntity> entitiesOnField;

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
            if(encounterConfig.enemies[i] != null)
            {
                var entity = Instantiate(encounterConfig.enemies[i], spawnPoints[i].position, Quaternion.identity);
                entity.id = i + 1;
            }
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


    // Events
    public static void CombatStart()
    {
        OnCombatStart?.Invoke();
    }
    public static void ActionStart()
    {
        OnActionStart?.Invoke();
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
    }
    private void OnDisable()
    {
        OnCombatStart -= SpawnEnemies;
        OnCombatStart -= GetEntities;
    }
}
