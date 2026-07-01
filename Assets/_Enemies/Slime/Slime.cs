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
        stats.actionPointsSpeed = baseStats.actionPointsSpeed;

        // Preps for combat
        actionPoints = 0f;
        canGainActionPoints = true;
    }
    public override IEnumerator Action()
    {
        // AI goes here

        // this way is too shit this gotta be changed
        int chanceToStrike = Random.Range(0, 2);
        if(chanceToStrike == 1)
        {
            CombatManager.instance.player.TakeDamage(2);
        }

        yield return new WaitForSeconds(2);
        actionPoints = 0;
    }
}
