using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CombatManager : MonoBehaviour
{
    #region Declarations
    public static CombatManager instance;
    public MainDatabase Database => MainDatabase.instance;

    [Header("References")]
    public Battle battle;
    public Deck deck;
    public Hand hand;
    public SkillExecutor executor;
    [Space]
    public Player player;
    public GameObject deathScreen;
    [Space]
    public bool entitiesAreActing = false;
    public List<IBattleEntity> entitiesOnField = new();
    public List<IBattleEntity> actingEntities = new();


    public SkillCard selectedCard;
    public Enemy selectedEnemy;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        instance = this;
        
        battle = GetComponent<Battle>();
        deck = GetComponent<Deck>();
        hand = GetComponent<Hand>();
        executor = GetComponent<SkillExecutor>();
    }
    private void Start()
    {
        deathScreen.SetActive(false);

        battle.SpawnEnemies();
        entitiesOnField = new List<IBattleEntity>(battle.GetEnemies());

        deck.InitDeck();
        deck.DrawCards(Database.nStartingCards);

        hand.Organize();
    }
    #endregion

    #region Methods
    public static void PerformActions()
    {
        instance.StartCoroutine(instance.PerformActionsCR(instance.actingEntities));
    }
    public IEnumerator PerformActionsCR(List<IBattleEntity> entities)
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

    public void SlowTime()
    {
        DOVirtual.Float
        (
            1,
            0,
            2f,
            value =>
            {
                Time.timeScale = value; ;
            }
        );
    }
    public void OpenDeathPanel()
    {
        deathScreen.SetActive(true);
        var panelGroup = deathScreen.GetComponent<CanvasGroup>();
        
        panelGroup.alpha = 0f;
        panelGroup.interactable = false;

        panelGroup.DOFade(1f, 0.5f)
            .SetEase(Ease.InQuad)
            .OnComplete(() => panelGroup.interactable = true);
    }
    #endregion
}
