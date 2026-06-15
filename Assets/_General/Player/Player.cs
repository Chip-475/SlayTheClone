using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour, IBattleEntity
{
    public class BattleAction
    {
        SkillCard card;
        Enemy target;

        public void Execute()
        {
            card.skill.Execute(target);
        }
    }

    public static event Action OnPlayerHealthChanged;

    [SerializeField] private PlayerStatsSO _baseStats;
    public int id;
    public bool canGainActionPoints;
    public float actionPoints;

    public int stamina;
    [SerializeField] private TMP_Text _staminaText;
    public static bool selecting;
    public bool isActing;
    bool playingCard = false;
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

        if (cardInUse != null && target != null && !playingCard) StartCoroutine(UseCard());
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
        playingCard = true;

        yield return StartCoroutine(cardInUse.skill.Execute(target));
        HandManager.instance.cardsInHand.Remove(cardInUse);
        Destroy(cardInUse.gameObject);
        HandManager.instance.SetCards(0.15f);

        cardInUse = null;
        target = null;
        selecting = false;

        playingCard = false;
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

    public IEnumerator Action()
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

    public void CalcDmg(int amount)
    {
        return;
    }
    public void TakeDamage(int amount)
    {
        _baseStats.hp -= amount;
        PlayerHealthChanged();
    }
}
