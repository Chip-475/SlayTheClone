using UnityEngine;
using System.Collections;

public class Molerat : Enemy
{
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

        yield return new WaitForSeconds(animLength);
        yield return StartCoroutine(BasicAttack());

        yield return new WaitForSeconds(animLength / 5);
        SwitchAnimation("Idle");

        actionPoints = 0;
    }

    IEnumerator BasicAttack()
    {
        // Deals damage in atk +- range
        int range = 2;
        int damageToDeal = Random.Range(info.atk - range, info.atk + range + 1);
        print(damageToDeal);

        // play animation ToDo
        CombatManager.instance.player.TakeDamage(damageToDeal);

        yield break;
    }
}
