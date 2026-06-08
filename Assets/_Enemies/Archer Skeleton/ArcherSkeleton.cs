using System.Collections;
using UnityEngine;

public class ArcherSkeleton : Enemy
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
