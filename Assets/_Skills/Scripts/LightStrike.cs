using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

// Light attack, possibly starter skill
[CreateAssetMenu(fileName = "LightStrike", menuName = "Scriptable Objects/Skills/LightStrike")]
public class LightStrike : SkillSO
{
    public override IEnumerator Execute(Enemy target)
    {
        Destroy(target.gameObject);

        BattleManager.CardPlayed();
        yield break;
    }
}
