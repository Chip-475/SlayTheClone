using UnityEngine;

public class LightSlash : Skill
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Effect(Enemy target)
    {
        int damage = PlayerManager.stats.atk * data.damagePercentage / 100;

        target.TakeDamage(damage);
        Manager.player.Stamina -= data.staminaCost;
    }
}
