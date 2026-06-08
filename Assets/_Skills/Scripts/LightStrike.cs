using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

// Light attack, possibly starter skill
[CreateAssetMenu(fileName = "LightStrike", menuName = "Scriptable Objects/Skills/LightStrike")]
public class LightStrike : SkillSO
{
    public override IEnumerator PlayCard(List<Enemy> targets)
    {
        foreach(Enemy enemy in targets)
        {
            Destroy(enemy.gameObject);
        }

        BattleManager.CardPlayed();
        yield break;
    }
}
