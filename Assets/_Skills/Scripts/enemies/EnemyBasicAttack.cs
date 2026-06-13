using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Basic Attack", menuName = "Scriptable Objects/Skills/Enemy Skills/Basic Attack")]
public class EnemyBasicAttack : SkillSO
{
    public override IEnumerator PlayCard(IBattleEntity caster, List<IBattleEntity> targets)
    {
        foreach (IBattleEntity target in targets)
        {
            target.TakeDamage(Random.Range(atkMin, atkMax + 1));
        }
        yield break;
    }
}
