using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerStatsSO _playerStats;
    Image _bar;

    private void Awake()
    {
        _bar = GetComponent<Image>();
    }

    void SetHealthBarFillAmount()
    {
        _bar.fillAmount = _playerStats.hp / _playerStats.maxHp;
    }

    private void OnEnable()
    {
        Player.OnPlayerDamaged += SetHealthBarFillAmount;
    }
    private void OnDisable()
    {
        Player.OnPlayerDamaged -= SetHealthBarFillAmount;
    }
}
