using UnityEngine;

public class RestManager : MonoBehaviour
{
    #region Declarations
    public static RestManager instance;
    public MainDatabase Database => MainDatabase.instance;

    public CraftMenu craftMenu;

    public int time;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        instance = this;

        craftMenu = GetComponent<CraftMenu>();
    }
    #endregion

    #region Methods
    public void DecreaseTime(int amount)
    {
        time -= amount;
    }
    #endregion
}
