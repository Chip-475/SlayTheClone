using UnityEngine;
using System.Collections;

public class ArcherSkeleton : Enemy
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
        // temporary
        BasicAttack();
        print($"{gameObject.name} has attacked!");


        yield return new WaitForSeconds(1); // To remove once animation is implemented

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
