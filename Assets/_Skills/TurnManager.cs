using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;

    public List<IBattleEntity> actingEntities = new();
    bool isPerforming;

    private void Start()
    {
        instance = this;
        isPerforming = false;
    }
    private void LateUpdate()
    {
        if (actingEntities != null && !isPerforming)
        {
            StartCoroutine(PerformActions());
        }
    }

    IEnumerator PerformActions()
    {
        isPerforming = true;

        StopActionBars();
        actingEntities.Sort((a, b) => a.GetId().CompareTo(b.GetId()));
        foreach (var entity in actingEntities)
        {
            print("entity cycled");
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
