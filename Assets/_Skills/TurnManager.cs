using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;

    public List<IBattleEntity> actingEntities = new();
    bool isPerforming = false;

    private void Start()
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
            yield return StartCoroutine(entity.BattleAction());
        }
        actingEntities.Clear();
        StartActionBars();

        isPerforming = false;
    }

    void StopActionBars()
    {
        foreach (var entity in BattleManager.instance.entitiesOnField)
        {
            entity.StopActionBar();
        }
    }
    void StartActionBars()
    {
        foreach (var entity in BattleManager.instance.entitiesOnField)
        {
            entity.StartActionBar();
        }
    }
}
