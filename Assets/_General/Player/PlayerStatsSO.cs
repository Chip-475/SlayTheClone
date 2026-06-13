using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Scriptable Objects/Stats/Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    public int hp;
    public int maxHp;
    public int actionPointsSpeed;
    public int res = new int();//0 blunt 1 fire 2 ice 3 magic 4 pierce 5 slash
}
