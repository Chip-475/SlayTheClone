using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerStatsSO _playerStats;
    Image _bar;
    AnimationCurve curve;

    private void Awake()
    {
        _bar = GetComponent<Image>();
        curve= AnimationCurve.EaseInOut(0, 0, _playerStats.maxHp, 1);
        curve.preWrapMode = WrapMode.PingPong;
        curve.postWrapMode = WrapMode.PingPong;
    }

    void SetHealthBarFillAmount()
    {
        _bar.fillAmount =curve.Evaluate(_playerStats.hp);

    }

    private void OnEnable()
    {
        Player.OnPlayerHealthChanged += SetHealthBarFillAmount;
    }
    private void OnDisable()
    {
        Player.OnPlayerHealthChanged -= SetHealthBarFillAmount;
    }
}
