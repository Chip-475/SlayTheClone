using UnityEngine;

public class MajorHeal : Skill
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Effect(Player target)
    {
        int toHeal = PlayerManager.P_Stats.atk * data.damagePercentage / 100;

        PlayerManager.player.ChangeAnimation("Cast");
        SpawnAnim(target.transform.position, data.skillName);
        target.TakeDamage(toHeal);
        Manager.player.Stamina -= data.staminaCost;
    }
}
