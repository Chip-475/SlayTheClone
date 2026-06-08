using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

// Light attack, possibly starter skill
[CreateAssetMenu(fileName = "LightStrike", menuName = "Scriptable Objects/Skills/LightStrike")]
public class LightStrike : SkillSO
{
    public override IEnumerator PlayCard(IBattleEntity caster, List<IBattleEntity> targets)
    {
        foreach(IBattleEntity target in targets)
        {
            int damage= Random.Range(atkMax, atkMin);
            target.TakeDamage(damage);
        }

        BattleManager.CardPlayed();
        yield break;
    }
}
