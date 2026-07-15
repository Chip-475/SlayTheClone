using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using Unity.Mathematics;
using System;
using System.Linq;

// Enemy base class
public abstract class Enemy : MonoBehaviour, IBattleEntity, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    #region Declarations
    [SerializeField] protected EnemyStatsSO baseStats;
    public EnemyStatsSO stats;

    MainDatabase Database => CombatManager.instance.Database;

    #region Non Variables
    public enum Mood
    {
        Neutral,
        Aggressive,
        Defensive,
        Desperate,
        Doordinated
    }
    public enum BattlePlan
    {
        attack,
        heal,
        buff,
        debuff,
        defend
    }
    [System.Serializable]
    public struct Awareness
    {
        [Header("hp")]
        public int currentHP;
        public int maxHP;

        [Header("playerStats")]
        public int playerHP;
        public int playerMaxHP;
        public int playerMoney;

        [Header("Allies")]
        public int aliveAllies;
        public int totalAllies;
        public bool isAnHealerAlly;
        public bool isASupporterAlly;
        public bool isADPSAlly;
        public bool isATankAlly;

        [Header("type")]
        public bool isBoss;
        public bool isElite;
        public bool isEnemy;

        [Header("killInfo")]
        public bool canKillPlayer;
        public bool canBeKilled;
        public bool AlliesCanBeKilled;

        [Header("actions")]
        public bool canHeal;
        public bool canBuff;
        public bool canDebuffPlayer;
        public bool canSummon;

        [Header("dmgInfo")]
        public int avgPlayerDamage;
        public int avgDmgToPlayer;
        public int totalPersonalDmgToPlayer;

        [Header("lastTurnInfo")]
        public bool wasAttackedLastTurn;
        public bool wasAllyKilledLastTurn;
        public bool wasHealedLastTurn;
        public bool didPlayerHealLastTurn;
        public bool didPlayerBuffLastTurn;

        public Mood currentMood;
        public BattlePlan currentBattlePlan;
    }
    [System.Serializable]
    public struct Drop
    {
        public ItemSO item;
        public int dropChance;
        public int minAmount;
        public int maxAmount;
    }
    #endregion Non Variables

    public Awareness awareness = new();
    public List<Drop> itemPool = new();
    public float actionPoints;
    public bool canGainActionPoints;
    public int id;

    public List<SkillSO> skillList = new();
    [Space]

    public Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Color baseColor;
    protected bool selected;

    EnemyBars bars;
    #endregion Declarations

    #region Unity Methods
    public virtual void Awake()
    {
        stats = Instantiate(baseStats);
    }
    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bars = GetComponent<EnemyBars>();
    }
    protected virtual void FixedUpdate()
    {
        if (canGainActionPoints)
        {
            actionPoints += stats.actionPointsSpeed * Time.deltaTime;
        }
        if(actionPoints >= 100 && !CombatManager.instance.actingEntities.Contains(this))
        {
            CombatManager.instance.actingEntities.Add(this);
        }
    }

    public abstract void OnEnable();
    public abstract void OnDisable();
    #endregion

    #region Methods
    public void SetInitialState()
    {
        // Preps for combat
        actionPoints = 0f;
        canGainActionPoints = true;
    }
    public void DropItems()
    {
        List<Drop> droppedItems = new();

        foreach(var drop in itemPool)
        {
            int r = UnityEngine.Random.Range(0, 101);
            if (r <= drop.dropChance) droppedItems.Add(drop);
        }

        foreach(var drop in droppedItems)
        {
            drop.item.amount += UnityEngine.Random.Range(drop.minAmount, drop.maxAmount + 1);
        }
    }

    #region Interface
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

    public abstract IEnumerator Action();

    public int CalcDmg(int damage)
    {
        float finalDamage = 0;

        var damageValues = CombatManager.instance.selectedCard.skill.damageTable.Values().ToList();
        var resistanceValues = baseStats.resistances.Values().ToList();
        

        for (int i = 0; i < damageValues.Count; i++)
        {
            finalDamage += damageValues[i] * resistanceValues[i];
        }

        return (int)Math.Ceiling(finalDamage);
    }
    public void TakeDamage(int damage)
    {
        print(damage);
        stats.hp -= CalcDmg(damage);
        bars.SetHealthBarFillAmount();
        if (stats.hp <= 0)
        {
            DropItems();
            Destroy(gameObject);
        }
    }
    #endregion Interface

    #endregion Methods

    #region Events
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!Player.selecting) return;

        CombatManager.instance.selectedEnemy = this;

        CombatManager.instance.executor.Execute();
    }
    #endregion
}
