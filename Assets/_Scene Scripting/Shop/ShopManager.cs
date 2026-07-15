using UnityEngine;

public class ShopManager : MonoBehaviour
{
    #region Declarations
    public static ShopManager instance;

    MainDatabase Database => MainDatabase.instance;

    ShopMoneyCounter moneyCounter;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        instance = this;

        moneyCounter = GetComponent<ShopMoneyCounter>();
    }
    private void Start()
    {
        moneyCounter.UpdateCounter();
    }
    #endregion
}
