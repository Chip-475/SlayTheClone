using UnityEngine;
using UnityEngine.UI;

public class PlayerBars : MonoBehaviour
{
    [SerializeField] Image hpBar;
    [SerializeField] Image apBar;
    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {
        SetHealthBarFillAmount();
        SetActionBarFillAmount();
    }

    private void SetHealthBarFillAmount()
    {
        float amount = (float)player.stats.hp / player.stats.maxHp;
        hpBar.fillAmount = amount;
    }
    private void SetActionBarFillAmount()
    {
        var amount = player.actionPoints / 100;
        apBar.fillAmount = amount;
    }
}
