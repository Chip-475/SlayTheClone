using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
    #region Declarations
    public static CombatManager instance;

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
            StartCoroutine(battle.PerformActions(actingEntities));
        }
    }
    #endregion

    #region Methods
    void InitBattle()
    {
        battle.SpawnEnemies();
        entitiesOnField = new List<IBattleEntity>(battle.GetEnemies());
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
