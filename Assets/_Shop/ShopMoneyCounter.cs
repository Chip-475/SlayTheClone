using UnityEngine;
using TMPro;

#pragma warning disable
public class ShopMoneyCounter : MonoBehaviour
{
    public int money => DB.instance.database.inventory.money;
    public TMP_Text counterText;

    public void UpdateCounter()
    {
        counterText.text = money.ToString();
    }
}
