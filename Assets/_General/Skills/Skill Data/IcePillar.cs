using UnityEngine;

public class IcePillar : Skill
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Effect(Enemy target)
    {
        int damage = PlayerManager.P_Stats.atk * data.damagePercentage / 100;

        PlayerManager.player.ChangeAnimation("Cast");
        SpawnAnim(target.transform.position, data.skillName);
        target.TakeDamage(damage);
        Manager.player.Stamina -= data.staminaCost;
    }
}
