using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Light attack, possibly starter skill
[CreateAssetMenu(fileName = "Swipe", menuName = "Scriptable Objects/Skills/Swipe")]
public class Swipe : Skill
{
    public override IEnumerator OnUse(List<Enemy> targets)
    {
        foreach (Enemy enemy in targets)
        {
            Destroy(enemy.gameObject);
        }

        Debug.Log("Swipe used successfully");
        yield return null;
    }
}
