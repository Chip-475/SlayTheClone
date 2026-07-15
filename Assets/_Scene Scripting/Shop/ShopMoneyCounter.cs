using UnityEngine;
using TMPro;
using UnityEngine.UI;

#pragma warning disable
public class ShopMoneyCounter : MonoBehaviour
{
    public int money => MainDatabase.instance.inventory.money;

    public Image moneyIcon;
    public TMP_Text counterText;

    public void UpdateCounter()
    {
        counterText.text = money.ToString();
    }
}
