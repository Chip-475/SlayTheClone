using UnityEngine;

public class LightAttack : Skill
{
    public override void Effect(Enemy target)
    {
        target.TakeDamage(PlayerManager.stats.atk);
        Manager.player.Stamina -= staminaCost;
    }
}
