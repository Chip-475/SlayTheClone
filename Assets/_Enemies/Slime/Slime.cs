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
    new void OnEnable()
    {
        base.OnEnable();
    }
    new void OnDisable()
    {
        base.OnDisable();
    }

    public override IEnumerator BattleAction()
    {
        print($"{gameObject.name}: {id} has acted.");
        yield return new WaitForSeconds(2);
    }
}

//Player player = FindFirstObjectByType<Player>();
//SkillSO chosenSkill = skillList[UnityEngine.Random.Range(0, skillList.Count)];
//List<IBattleEntity> targets = new() { player };
//yield return chosenSkill.PlayCard(this, targets);
//actionBarAmount = 0;