using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IBattleEntity
{
    public static event Action OnPlayerHealthChanged;

    [SerializeField] private PlayerStatsSO _baseStats;
    public int id;
    public bool canGainActionPoints;
    public float actionPoints;

    private void FixedUpdate()
    {
        if(canGainActionPoints) actionPoints += _baseStats.spdPerSecond * Time.deltaTime;
        if (actionPoints >= 100 && !TurnManager.instance.actingEntities.Contains(this))
        {
            TurnManager.instance.actingEntities.Add(this);
        }
    }

    //
    void SetInitialState()
    {
        actionPoints = 0;
        canGainActionPoints = true;
        id = 0;
    }

    // Events
    public static void PlayerHealthChanged()
    {
        OnPlayerHealthChanged?.Invoke();
    }

    // Interface
    public int GetId()
    {
        return id;
    }
    public void StopActionBar()
    {
        canGainActionPoints = false;
    }
    public void StartActionBar()
    {
        canGainActionPoints = true;
    }

    public IEnumerator BattleAction()
    {
        print($"{gameObject.name}: {id} has acted.");
        yield return new WaitForSeconds(2);
        actionPoints = 0;
    }
    public void TakeDamage(int amount)
    {
        _baseStats.hp -= amount;
        PlayerHealthChanged();
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
