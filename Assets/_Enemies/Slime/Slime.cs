using UnityEngine;
using System.Collections;

public class Slime : Enemy
{
    #region Unity Methods
    new void Start()
    {
        base.Start();
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

    public override void SetInitialState()
    {
        // Clone stats from asset to local class to avoid modifying all enemies
        stats.hp = baseStats.hp;
        stats.maxHp = baseStats.maxHp;
        stats.atk = baseStats.atk;
        stats.actionPointsSpeed = baseStats.actionPointsSpeed;

        // Preps for combat
        actionPoints = 0f;
        canGainActionPoints = true;
    }
    public override IEnumerator Action()
    {
        int temp = Random.Range(0, 2);
        if(temp == 0)
        {
            yield return StartCoroutine(BasicAttack());
            print($"{gameObject.name} attacks!");
        }
        else
        {
            print($"{gameObject.name} does nothing!");
        }

        actionPoints = 0;
    }

    IEnumerator BasicAttack()
    {
        // Deals damage between atk + 1 and atk - 1 
        int range = 1;
        int damageToDeal = Random.Range(stats.atk - range, stats.atk + range + 1);
        print(damageToDeal);

        // play animation ToDo
        CombatManager.instance.player.TakeDamage(damageToDeal);

        yield break;
    }
}
