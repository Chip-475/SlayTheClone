using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Light attack, possibly starter skill
[CreateAssetMenu(fileName = "Leg Sweep", menuName = "Scriptable Objects/Skills/Leg Sweep")]
public class LegSweep : SkillSO
{
    public override IEnumerator Effect(Enemy target)
    {
        int damageToDeal = Random.Range(atkMin, atkMax + 1);
        target.TakeDamage(damageToDeal);

        BattleManager.CardPlayed();
        yield break;
    }
}
