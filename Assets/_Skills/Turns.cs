using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turns : MonoBehaviour
{
    public static Turns instance;

    public List<IBattleEntity> actingEntities = new();
    bool isPerforming = false;

    private void Awake()
    {
        instance = this;
    }
    private void LateUpdate()
    {
        if (actingEntities.Count > 0  && !isPerforming)
        {
            isPerforming = true;
            StartCoroutine(PerformActions());
        }
    }

    IEnumerator PerformActions()
    {
        StopActionBars();
        actingEntities.Sort((a, b) => a.GetId().CompareTo(b.GetId()));
        foreach (var entity in actingEntities)
        {
            yield return StartCoroutine(entity.Action());
        }
        actingEntities.Clear();
        StartActionBars();

        isPerforming = false;
    }

    void StopActionBars()
    {
        foreach (var entity in Battle.instance.entitiesOnField)
        {
            entity.StopActionBar();
        }
    }
    void StartActionBars()
    {
        foreach (var entity in Battle.instance.entitiesOnField)
        {
            entity.StartActionBar();
        }
    }
}
