using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Light attack, possibly starter skill
[CreateAssetMenu(fileName = "Leg Sweep", menuName = "Scriptable Objects/Skills/Leg Sweep")]
public class LegSweep : SkillSO
{
    public override IEnumerator PlayCard(IBattleEntity caster,List<IBattleEntity> targets)
    {
        foreach (Enemy enemy in targets)
        {
            Destroy(enemy.gameObject);
        }

        BattleManager.CardPlayed();
        yield break;
    }
}
