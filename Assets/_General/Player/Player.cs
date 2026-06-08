using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IBattleEntity
{
    [SerializeField] private PlayerStatsSO _baseStats;
    public float actionBarAmount;
    bool _actionBarCanMove;
    public int id;

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
        print($"{gameObject.name}: {id} has acted.");
        yield return new WaitForSeconds(2);
    }
    public void TakeDamage(int amount)
    {
        _baseStats.hp -= amount;
        PlayerDamaged();
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
