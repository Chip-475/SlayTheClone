using UnityEngine;

public class LightAttack : Skill
{
    public new void Effect(Enemy target)
    {
        target.TakeDamage(PlayerManager.instance.stats.atk);
    }
}
