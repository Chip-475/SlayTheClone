using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Scriptable Objects/Stats/Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    public int hp;
    public int maxHp;
    public int actionPointsSpeed;
    public int[] res;
}
