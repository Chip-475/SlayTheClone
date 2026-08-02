using UnityEngine;

public class HeavyAttack : Skill
{
    public override void Effect(Enemy target)
    {
        target.TakeDamage(PlayerManager.stats.atk);
        Manager.player.Stamina -= staminaCost;
    }
}

