using UnityEngine;
using UnityEngine.UI;
public class hpBar : MonoBehaviour
{
    Image _bar;
    AnimationCurve curve;
    Enemy enemy;
    private void Awake()
    {
        _bar = GetComponent<Image>();
        enemy= GetComponentInParent<Enemy>();
        curve = AnimationCurve.EaseInOut(0, 0,enemy.stats.hp, 1);
        curve.preWrapMode = WrapMode.PingPong;
        curve.postWrapMode = WrapMode.PingPong;
    }

    public void SetHealthBarFillAmount()
    {
        _bar.fillAmount = curve.Evaluate(enemy.stats.hp);
    }
}
