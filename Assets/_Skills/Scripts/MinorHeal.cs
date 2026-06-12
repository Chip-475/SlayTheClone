using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Minor Heal", menuName = "Scriptable Objects/Skills/Minor Heal")]
public class MinorHeal : SkillSO
{
    public override IEnumerator PlayCard(IBattleEntity caster, List<IBattleEntity> targets)
    {
        yield break;
    }
}
