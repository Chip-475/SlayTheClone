using UnityEngine;
using System.Collections;

public class SwordsmanSkeleton : Enemy
{
    new void Start()
    {
        base.Start();
    }
    new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void SetInitialState()
    {
        // Clone stats from asset to local class to avoid modifying all enemies
        stats.hp = baseStats.hp;
        stats.maxHp = baseStats.maxHp;
        stats.actionPointsSpeed = baseStats.actionPointsSpeed;

        // Preps for combat
        actionPoints = 0f;
        canGainActionPoints = true;
    }

    public override IEnumerator BattleAction()
    {
        yield break;
    }

    // Management
    public override void OnEnable()
    {
        
    }
    public override void OnDisable()
    {
        
    }
}
