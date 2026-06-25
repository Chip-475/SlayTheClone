using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Guard", menuName = "Scriptable Objects/Skills/Guard")]
public class Guard : SkillSO
{
    public override IEnumerator Effect(Enemy target)
    {
        yield break;
    }
}
