using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

public class Battle : MonoBehaviour
{
    #region Declarations
    public static event Action OnActionStart;
    public static event Action OnCardPlayed;



    [SerializeField] List<Transform> spawnPoints = new();
    [SerializeField] EncounterConfigSO encounterConfig;
    
    #endregion

    #region Methods
    public void SpawnEnemies()
    {
        for(int i = 0; i < encounterConfig.enemies.Count; i++)
        {
            if(encounterConfig.enemies[i] != null)
            {
                var entity = Instantiate(encounterConfig.enemies[i], spawnPoints[i].position, Quaternion.identity);
                entity.id = i + 1;
            }
        }
    }

    public List<IBattleEntity> GetEnemies()
    {
        var toReturn = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IBattleEntity>()
            .ToList();
        return new List<IBattleEntity>(toReturn);
    }

    public IEnumerator PerformActions(List<IBattleEntity> entities)
    {
        CombatManager.instance.entitiesAreActing = true;

        StopActionBars();
        entities.Sort((a, b) => a.GetId().CompareTo(b.GetId()));
        foreach (var entity in entities)
        {
            yield return StartCoroutine(entity.Action());
        }
        entities.Clear();
        StartActionBars();

        CombatManager.instance.entitiesAreActing = false;
    }

    public void StopActionBars()
    {
        foreach (var entity in CombatManager.instance.entitiesOnField)
        {
            entity.StopActionBar();
        }
    }
    public void StartActionBars()
    {
        foreach (var entity in CombatManager.instance.entitiesOnField)
        {
            entity.StartActionBar();
        }
    }
    #endregion

    #region Event Methods
    public static void ActionStart()
    {
        OnActionStart?.Invoke();
    }
    public static void CardPlayed()
    {
        OnCardPlayed?.Invoke();
    }
    #endregion
}
