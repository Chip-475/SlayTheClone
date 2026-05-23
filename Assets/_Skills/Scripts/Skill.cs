using UnityEngine;

// Base class for all skills
public abstract class Skill : ScriptableObject
{
    public int cost;

    public int atkMin;
    public int atkMax;
    public float healMin;
    public float healMax;
    public float shield;

    public abstract void OnUse(GameObject target);
}
