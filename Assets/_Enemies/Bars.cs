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
    private void LateUpdate()
    {
        SetActionBarFillAmount();
    }

    public void SetHealthBarFillAmount()
    {
        _healthBar.fillAmount = _enemy.stats.hp / _enemy.stats.maxHp;
    }
    public void SetActionBarFillAmount()
    {
        _actionBar.fillAmount = _enemy.actionPoints / 100;
    }
}
