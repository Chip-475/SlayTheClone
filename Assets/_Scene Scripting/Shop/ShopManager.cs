using DG.Tweening;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    #region Declarations
    public static ShopManager instance;

    MainDatabase Database => MainDatabase.instance;

    public ShopMoneyCounter moneyCounter;

    public GameObject fadePanel;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        instance = this;

        moneyCounter = GetComponent<ShopMoneyCounter>();
    }
    private void Start()
    {
        fadePanel.SetActive(true);
        fadePanel.GetComponent<CanvasGroup>().DOFade(0, 0.3f);

        moneyCounter.UpdateCounter();
    }
    #endregion
}
