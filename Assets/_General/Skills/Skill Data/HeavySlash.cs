using UnityEngine;

public class HeavySlash : Skill
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Effect(Enemy target)
    {
        int damage = PlayerManager.stats.atk * data.damagePercentage / 100;

        Database.instance.skillAnim.Instantiate(target.transform.position, data.skillName);
        target.TakeDamage(damage);
        Manager.player.Stamina -= data.staminaCost;
    }
}

