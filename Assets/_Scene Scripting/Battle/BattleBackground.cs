using UnityEngine;
using System.Collections.Generic;

public class BattleBackground : MonoBehaviour
{
    public Transform playerPosition;
    public List<Transform> spawnPoints;

    private void Awake()
    {
        CombatManager.instance.battle.playerPosition = playerPosition;
        CombatManager.instance.battle.spawnPoints.AddRange(spawnPoints);
    }
}
