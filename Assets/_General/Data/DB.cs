using UnityEngine;

public class DB : MonoBehaviour
{
    public static DB instance;

    public DatabaseSO database;
    public PlayerStatsSO playerStats;

    private void Awake()
    {
        instance = this;
    }
}
