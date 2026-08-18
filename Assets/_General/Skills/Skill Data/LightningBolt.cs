using UnityEngine;

public class LightningBolt : Skill
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Effect(Enemy target)
    {
        int damage = PlayerManager.P_Stats.atk * data.damagePercentage / 100;

        SpawnAnim(target.transform.position, data.skillName);
        target.TakeDamage(damage);
        // Add lightning infliction
        Manager.player.Stamina -= data.staminaCost;
    }
}
