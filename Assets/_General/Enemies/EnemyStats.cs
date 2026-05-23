using UnityEngine;

// Stores stat data for every enemy
[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/Stats/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public int hp;
    public int atk;
}