using UnityEngine;

// Stores stat data for every enemy
[CreateAssetMenu(fileName = "Enemy Stats", menuName = "Scriptable Objects/Stats/Enemy Stats")]
public class EnemyStatsSO : ScriptableObject
{
    public int hp;
    public int maxHp;
    public int actionPointsSpeed;
    public int res = new int();//0 blunt 1 fire 2 ice 3 magic 4 pierce 5 slash
}