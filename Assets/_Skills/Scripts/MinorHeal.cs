using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Minor Heal", menuName = "Scriptable Objects/Skills/Minor Heal")]
public class MinorHeal : SkillSO
{
    public override IEnumerator Execute(IBattleEntity caster, List<IBattleEntity> targets)
    {
        yield break;
    }
}
