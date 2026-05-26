using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Base class for all skills
public abstract class Skill : ScriptableObject
{
    public int cost;
    public int numberOfTargets;

    public int atkMin;
    public int atkMax;
    public float healMin;
    public float healMax;
    public float shield;

    public abstract IEnumerator OnUse(List<Enemy> targets);
}
