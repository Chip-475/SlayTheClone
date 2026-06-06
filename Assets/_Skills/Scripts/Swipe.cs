using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Light attack, possibly starter skill
[CreateAssetMenu(fileName = "Swipe", menuName = "Scriptable Objects/Skills/Swipe")]
public class Swipe : SkillSO
{
    public override IEnumerator PlayCard(List<Enemy> targets)
    {
        foreach (Enemy enemy in targets)
        {
            Destroy(enemy.gameObject);
        }

        BattleManager.CardPlayed();
        yield break;
    }
}
