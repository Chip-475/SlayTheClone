using UnityEngine;
using UnityEngine.UI;

#pragma warning disable
public class HealthBar : MonoBehaviour
{
    PlayerStats playerStats => PlayerManager.instance.stats;
    Image _bar;
    AnimationCurve curve;

    private void Awake()
    {
        _bar = GetComponent<Image>();
        curve= AnimationCurve.EaseInOut(0, 0, playerStats.maxHp, 1);
        curve.preWrapMode = WrapMode.PingPong;
        curve.postWrapMode = WrapMode.PingPong;
    }

    void SetHealthBarFillAmount()
    {
        _bar.fillAmount =curve.Evaluate(playerStats.hp);

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
