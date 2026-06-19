using UnityEngine;
using UnityEngine.UI;
public class Bars : MonoBehaviour
{
    Enemy _enemy;
    [SerializeField] Image _healthBar;
    [SerializeField] Image _actionBar;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }
    private void Update()
    {
        SetActionBarFillAmount();
    }

    public void SetHealthBarFillAmount()
    {
        _healthBar.fillAmount = (float)_enemy.stats.hp / (float)_enemy.stats.maxHp;
    }
    public void SetActionBarFillAmount()
    {
        _actionBar.fillAmount = (float)_enemy.actionPoints / 100;
    }
}
