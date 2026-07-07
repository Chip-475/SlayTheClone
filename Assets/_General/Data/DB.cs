using UnityEngine;

public class DB : MonoBehaviour
{
    public static DB instance;

    public DatabaseSO database;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
