using UnityEngine;
using UnityEngine.UI;

#pragma warning disable
public class HealthBar : MonoBehaviour
{
    Player player => PlayerManager.player;
    Image _bar;
    AnimationCurve curve;

    private void Awake()
    {
        _bar = GetComponent<Image>();
        curve = AnimationCurve.EaseInOut(0, 0, PlayerManager.P_Stats.maxHp, 1);
        curve.preWrapMode = WrapMode.PingPong;
        curve.postWrapMode = WrapMode.PingPong;
    }

    void SetHealthBarFillAmount()
    {
        _bar.fillAmount =curve.Evaluate(player.Health);

    }

    private void OnEnable()
    {
        Player.OnStaminaChanged += SetHealthBarFillAmount;
    }
    private void OnDisable()
    {
        Player.OnStaminaChanged -= SetHealthBarFillAmount;
    }
}
