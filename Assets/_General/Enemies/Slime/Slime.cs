using UnityEngine;
using System.Collections;

public class Slime : Enemy
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
        bool attack = Random.Range(0, 2) == 0;
        if(attack)
        {
            yield return StartCoroutine(BasicAttack());
            print($"{gameObject.name} attacks!");
        }
        else
        {
            print($"{gameObject.name} does nothing!");
        }

        yield return new WaitForSeconds(1); // To remove once animation is implemented

        actionPoints = 0;
    }

    IEnumerator BasicAttack()
    {
        // Deals damage in atk +- range
        int damageToDeal = Random.Range(info.atk - info.atkRange, info.atk + info.atkRange + 1);
        print(damageToDeal);

        // play animation ToDo
        CombatManager.instance.player.TakeDamage(damageToDeal);

        yield break;
    }
}
