using UnityEngine;
using System.Collections;

public class ShielderSkeleton : Enemy
{
    new void Start()
    {
        base.Start();
    }
    new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override IEnumerator BattleAction()
    {
        yield break;
    }
}
