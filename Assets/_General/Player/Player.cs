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

    [Range(0, 15)] public int stamina;
    bool isActing;

    private void FixedUpdate()
    {
        if(canGainActionPoints) actionPoints += _baseStats.actionPointsSpeed * Time.deltaTime;
        if (actionPoints >= 100 && !TurnManager.instance.actingEntities.Contains(this))
        {
            TurnManager.instance.actingEntities.Add(this);
        }
    }

    //
    void SetInitialState()
    {
        id = 0;
        actionPoints = 0;
        canGainActionPoints = true;
        
        stamina = 5;
        isActing = false;
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
        stamina += 3;
        isActing = true;
        yield return new WaitUntil(() => !isActing);
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
