using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

public class Lich : Enemy
{
    public GameObject attackEffect;

    #region Unity Methods
    new void Awake()
    {
        base.Awake();
    }
    new void Start()
    {
        base.Start();

        Init();
    }
    new void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void OnEnable()
    {

    }
    public override void OnDisable()
    {

    }
    #endregion

    public override IEnumerator Action()
    {
        bool ifAttack = Random.Range(0, 100) < 60;

        if (ifAttack)
        {
            SwitchAnimation("Attack");
            float animLength = GetAnimation("Attack").length;
            yield return new WaitForSeconds(animLength);

            SwitchAnimation("Idle");
        }

        actionPoints = 0;
    }
    public void DealDamage()
    {
        int damageToDeal = Random.Range(info.atk - info.atkRange, info.atk + info.atkRange + 1);
        CombatManager.instance.player.TakeDamage(damageToDeal);

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        float remaining = GetAnimation("Attack").length * (1f - (state.normalizedTime % 1f));

        var effectPosition = new Vector2(CombatManager.instance.player.transform.position.x + 0.5f, CombatManager.instance.player.transform.position.y + 1);
        var effect = Instantiate(attackEffect, effectPosition, Quaternion.identity);
        effect.GetComponent<LichAttackEffect>().destroyTime = remaining;
    }
}
