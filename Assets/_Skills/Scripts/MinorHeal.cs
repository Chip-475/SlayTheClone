using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Minor Heal", menuName = "Scriptable Objects/Skills/Minor Heal")]
public class MinorHeal : SkillSO
{
    public override IEnumerator Effect(Enemy target)
    {
        yield break;
    }
}
