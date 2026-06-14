using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Light attack, possibly starter skill
[CreateAssetMenu(fileName = "Leg Sweep", menuName = "Scriptable Objects/Skills/Leg Sweep")]
public class LegSweep : SkillSO
{
    public override IEnumerator Execute(Enemy target)
    {
        Destroy(target.gameObject);

        BattleManager.CardPlayed();
        yield break;
    }
}
