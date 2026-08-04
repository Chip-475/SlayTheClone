using UnityEngine;

public class HeavySlash : Skill
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Effect(Enemy target)
    {
        int damage = PlayerManager.stats.atk * damagePercentage / 100;

        target.TakeDamage(damage);
        Manager.player.Stamina -= staminaCost;
    }
}

