using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;

public class Slime : Enemy
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
        stats.hp = _baseStats.hp;
        stats.maxHp = _baseStats.maxHp;
        stats.spdPerSecond = _baseStats.actionPointsSpeed;

        // Preps for combat
        actionPoints = 0f;
        canGainActionPoints = true;
    }
    public override IEnumerator BattleAction()
    {
        print($"{gameObject.name}: {id} has acted.");
        yield return new WaitForSeconds(2);
        actionPoints = 0;
    }

    // Management
    public override void OnEnable()
    {
        
    }
    public override void OnDisable()
    {
        
    }
}

//Player player = FindFirstObjectByType<Player>();
//SkillSO chosenSkill = skillList[UnityEngine.Random.Range(0, skillList.Count)];
//List<IBattleEntity> targets = new() { player };
//yield return chosenSkill.PlayCard(this, targets);
//actionBarAmount = 0;