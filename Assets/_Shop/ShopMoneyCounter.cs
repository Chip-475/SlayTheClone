using UnityEngine;
using TMPro;
using UnityEngine.UI;

#pragma warning disable
public class ShopMoneyCounter : MonoBehaviour
{
    public int money => DB.instance.database.inventory.money;

    public Image moneyIcon;
    public TMP_Text counterText;

    public void UpdateCounter()
    {
        counterText.text = money.ToString();
    }
}
