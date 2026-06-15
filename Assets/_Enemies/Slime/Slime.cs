using UnityEngine;
using System.Collections;

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
        stats.hp = baseStats.hp;
        stats.maxHp = baseStats.maxHp;
        stats.actionPointsSpeed = baseStats.actionPointsSpeed;

        // Preps for combat
        actionPoints = 0f;
        canGainActionPoints = true;
    }
    public override IEnumerator Action()
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