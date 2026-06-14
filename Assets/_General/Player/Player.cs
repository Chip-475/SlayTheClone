using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, IBattleEntity
{
    public static event Action OnPlayerHealthChanged;

    [SerializeField] private PlayerStatsSO _baseStats;
    public int id;
    public bool canGainActionPoints;
    public float actionPoints;

    public int stamina;
    [SerializeField] private TMP_Text _staminaText;
    public static bool selecting;
    public bool isActing;
    public static SkillCard cardInUse = null;
    public static Enemy target = null;

    private void Start()
    {
        SetInitialState();
    }
    private void FixedUpdate()
    {
        if(canGainActionPoints) actionPoints += _baseStats.actionPointsSpeed * Time.deltaTime;
        if (actionPoints >= 100 && !TurnManager.instance.actingEntities.Contains(this))
        {
            TurnManager.instance.actingEntities.Add(this);
        }

        if (cardInUse != null && target != null) StartCoroutine(UseCard());
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
    public void StaminaChanged()
    {
        _staminaText.text = $"{stamina}";
    }
    public void EndTurn()
    {
        if (!isActing) return;

        isActing = false;
        print("Player turn ended.");
    }
    public IEnumerator UseCard()
    {
        yield return StartCoroutine(cardInUse.skill.Execute(target));
        HandManager.instance.cardsInHand.Remove(cardInUse);
        Destroy(cardInUse.gameObject);
        HandManager.instance.SetCards(0.15f);

        cardInUse = null;
        target = null;
        selecting = false;
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
        stamina = Math.Clamp(stamina ,0, 15);
        StaminaChanged();
        isActing = true;
        HandManager.instance.HideCards(false);
        yield return new WaitUntil(() => isActing == false);
        HandManager.instance.HideCards(true);
        actionPoints = 0;
    }

    public void CalcDmg(int amount, List<DamageTypeSO> types)
    {
        return;
    }
    public void TakeDamage(int amount)
    {
        _baseStats.hp -= amount;
        PlayerHealthChanged();
    }
}
