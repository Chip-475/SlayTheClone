using UnityEngine;

public class LightningBolt : Skill
{
    public new void Effect(Enemy target)
    {
        int damage = (PlayerManager.stats.atk * damagePercentage) / 100;

        target.TakeDamage(damage);
        // Add lightning infliction
        Manager.player.Stamina -= staminaCost;
    }
}
