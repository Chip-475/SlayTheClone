using UnityEngine;
using UnityEngine.UI;
public class EnemyBars : MonoBehaviour
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
        float amount = (float)_enemy.stats.hp / (float)_enemy.stats.maxHp;
        _healthBar.fillAmount = amount;
    }
    public void SetActionBarFillAmount()
    {
        float amount = (float)_enemy.actionPoints / 100;
        _actionBar.fillAmount = amount;
    }
}
