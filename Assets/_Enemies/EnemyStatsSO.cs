using UnityEngine;

// Stores stat data for every enemy
[CreateAssetMenu(fileName = "Enemy Stats", menuName = "Scriptable Objects/Stats/Enemy Stats")]
public class EnemyStatsSO : ScriptableObject
{
    public int hp;
    public int maxHp;
    public int actionPointsSpeed;
}