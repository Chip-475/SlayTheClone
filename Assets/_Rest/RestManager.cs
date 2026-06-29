using UnityEngine;

public class RestManager : MonoBehaviour
{
    #region Declarations
    public static RestManager instance;
    public DatabaseSO Database => DB.instance.database;

    public int time;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region Methods
    public static void DecreaseTime(int amount)
    {
        instance.time -= amount;
    }
    #endregion
}
