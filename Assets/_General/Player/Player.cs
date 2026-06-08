using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IBattleEntity
{
    [SerializeField] private PlayerStatsSO _baseStats;
    public float actionBarAmount;
    bool _actionBarCanMove;

    public static event Action OnPlayerDamaged;

    private void FixedUpdate()
    {
        if(_actionBarCanMove) actionBarAmount += _baseStats.spdPerSecond;
    }

    //
    void SetInitialState()
    {
        actionBarAmount = 0;
        _actionBarCanMove = true;
    }

    // Events
    public static void PlayerDamaged()
    {
        OnPlayerDamaged?.Invoke();
    }

    // Interface
    public IEnumerator BattleAction()
    {
        yield return null;
    }
    public void StopActionBar()
    {
        _actionBarCanMove = false;
    }
    public void StartActionBar()
    {
        _actionBarCanMove = false;
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
