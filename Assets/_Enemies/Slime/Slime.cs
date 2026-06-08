using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

public class Slime : Enemy
{
    new void Start()
    {
        base.Start();
    }
    public override IEnumerator BattleAction()
    {
        Player player = FindFirstObjectByType<Player>();
        SkillSO chosenSkill = skillList[UnityEngine.Random.Range(0,skillList.Count)];
        List<IBattleEntity> targets = new() { player };
        yield return chosenSkill.PlayCard(this, targets);
        actionBarAmount = 0;
    }
}
