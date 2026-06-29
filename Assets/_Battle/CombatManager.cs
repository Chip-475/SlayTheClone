using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
    #region Declarations
    public static CombatManager instance;
    public DatabaseSO Database => DB.instance.database;

    //public static event Action OnCardPlayed;

    [Header("Essentials")]
    public Battle battle;
    public Deck deck;
    public Hand hand;
    [Space]
    public Player player;

    public bool entitiesAreActing = false;
    public List<IBattleEntity> entitiesOnField = new();
    public List<IBattleEntity> actingEntities = new();
    #endregion

    #region Unity Methods
    private void Awake()
    {
        instance = this;
        CheckValidity();
    }
    private void Start()
    {
        InitBattle();
    }
    private void LateUpdate()
    {
        if (actingEntities.Count > 0 && !entitiesAreActing)
        {
            StartCoroutine(PerformActions(actingEntities));
        }
    }
    #endregion

    #region Methods
    void InitBattle()
    {
        battle.SpawnEnemies();
        entitiesOnField = new List<IBattleEntity>(battle.GetEnemies());

        deck.FillDeck();
        deck.DrawCards(Database.nStartingCards);

        hand.Organize();
    }

    public IEnumerator PerformActions(List<IBattleEntity> entities)
    {
        entitiesAreActing = true;

        StopActionBars();
        entities.Sort((a, b) => a.GetId().CompareTo(b.GetId()));
        foreach (var entity in entities)
        {
            yield return StartCoroutine(entity.Action());
        }
        entities.Clear();
        StartActionBars();

        entitiesAreActing = false;
    }

    public static void Draw()
    {
        instance.deck.DrawCards(instance.Database.nCardsAtTurnStart);
        instance.hand.Organize();
    }

    public void StopActionBars()
    {
        foreach (var entity in entitiesOnField)
        {
            entity.StopActionBar();
        }
    }
    public void StartActionBars()
    {
        foreach (var entity in entitiesOnField)
        {
            entity.StartActionBar();
        }
    }
    #endregion

    #region Helpers
    void CheckValidity()
    {
        if (battle == null) throw new System.ArgumentNullException("battle");
        if (deck == null) throw new System.ArgumentNullException("deck");
        if (hand == null) throw new System.ArgumentNullException("hand");
    }
    #endregion
}
