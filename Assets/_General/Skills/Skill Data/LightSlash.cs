using UnityEngine;

public class LightSlash : Skill
{
    public new void Effect(Enemy target)
    {
        int damage = (PlayerManager.stats.atk * damagePercentage) / 100;

        target.TakeDamage(damage);
        Manager.player.Stamina -= staminaCost;
    }
}
