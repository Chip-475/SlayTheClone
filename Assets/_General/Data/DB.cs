using UnityEngine;

public class DB : MonoBehaviour
{
    public static DB instance;

    public DatabaseSO database;

    private void Awake()
    {
        instance = this;
    }
}
