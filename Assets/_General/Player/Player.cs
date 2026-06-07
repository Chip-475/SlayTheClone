using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IBattleEntity
{
    [SerializeField] private PlayerStatsSO _baseStats;

    public static event Action OnPlayerDamaged;

    public float actionBarAmount;
    bool _actionBarCanMove;
    public int id;

    private void FixedUpdate()
    {
        if(_actionBarCanMove) actionBarAmount += _baseStats.spdPerSecond * Time.deltaTime;
        if(actionBarAmount >= 100)
        {
            BattleManager.instance.actingEntities.Add(this);
        }
    }

    //
    void SetInitialState()
    {
        actionBarAmount = 0;
        _actionBarCanMove = true;
        id = 0;
    }

    // Events
    public static void PlayerDamaged()
    {
        OnPlayerDamaged?.Invoke();
    }

    // Interface
    public int GetId()
    {
        return id;
    }
    public void StopActionBar()
    {
        _actionBarCanMove = false;
    }
    public void StartActionBar()
    {
        _actionBarCanMove = false;
    }
    public IEnumerator BattleAction()
    {
        yield break;
    }

    // Management
    private void OnEnable()
    {
        BattleManager.OnCombatStart += SetInitialState;
    }
    private void OnDisable()
    {
        BattleManager.OnCombatStart -= SetInitialState;
    }
}
