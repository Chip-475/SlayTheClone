using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Light attack, possibly starter skill
[CreateAssetMenu(fileName = "LightStrike", menuName = "Scriptable Objects/Skills/LightStrike")]
public class LightStrike : Skill
{
    public override IEnumerator OnUse(List<Enemy> targets)
    {
        foreach(Enemy enemy in targets)
        {
            Destroy(enemy.gameObject);
        }

        Debug.Log("Light Strike used successfully");
        yield return null;
    }
}
