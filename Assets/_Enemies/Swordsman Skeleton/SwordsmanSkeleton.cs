using UnityEngine;
using System.Collections;

public class SwordsmanSkeleton : Enemy
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
