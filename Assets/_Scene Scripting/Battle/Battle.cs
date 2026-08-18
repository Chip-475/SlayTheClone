using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

public class Battle : MonoBehaviour
{
    #region Declarations
    public List<BattleBackground> backgrounds = new();
    public Transform playerPosition;
    public List<Transform> spawnPoints = new();
    [SerializeField] List<EncounterConfigSO> encounterConfigs;
    [SerializeField] List<EncounterConfigSO> bossConfigs;
    #endregion

    #region Methods
    public void SpawnBackground()
    {
        int index = UnityEngine.Random.Range(0, backgrounds.Count);

        Instantiate(backgrounds[index], new Vector2(0, 0), Quaternion.identity);
    }
    public void SpawnEnemies()
    {
        CombatManager.instance.player.transform.position = playerPosition.position;
        EncounterConfigSO currentConfig;
        if (Database.bossNodeClicked)
        {
            currentConfig = bossConfigs[UnityEngine.Random.Range(0, bossConfigs.Count)];
            Database.bossNodeClicked = false;
        }
        else
        {
            currentConfig = encounterConfigs[UnityEngine.Random.Range(0, encounterConfigs.Count)];
        }

        for (int i = 0; i < currentConfig.enemies.Count; i++)
        {
            if(currentConfig.enemies[i] != null && spawnPoints[i] != null)
            {
                var entity = Instantiate(currentConfig.enemies[i], spawnPoints[i].position, Quaternion.identity);
                entity.id = i + 1;
            }
        }
    }
    #endregion
}
