using UnityEngine;
using System.Collections;

public class Bat : Enemy
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

        SetInitialState();
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
            yield return StartCoroutine(BasicAttack());
            SwitchAnimation("Idle");
        }

        actionPoints = 0;
        yield break;
    }

    IEnumerator BasicAttack()
    {
        // Deals damage in atk +- range
        int damageToDeal = Random.Range(info.atk - info.atkRange, info.atk + info.atkRange + 1);
        SwitchAnimation("Attack");

        float animLength = 0f;
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Attack")
            {
                animLength = clip.length;
                break;
            }
        }

        var effectPosition = new Vector2(CombatManager.instance.player.transform.position.x + 0.5f, CombatManager.instance.player.transform.position.y + 1);
        var effect = Instantiate(attackEffect, effectPosition, Quaternion.identity);
        effect.GetComponent<BatAttackEffect>().destroyTime = animLength;

        CombatManager.instance.player.TakeDamage(damageToDeal);

        yield return new WaitForSeconds(animLength);
        yield break;
    }
}
