using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

public class Battle : MonoBehaviour
{
    #region Declarations
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
    #endregion
}
