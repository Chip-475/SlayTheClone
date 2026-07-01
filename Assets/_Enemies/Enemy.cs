using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;

// Enemy base class
public abstract class Enemy : MonoBehaviour, IBattleEntity, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    #region Declarations
    [SerializeField] protected EnemyStatsSO baseStats;

    DatabaseSO Database => CombatManager.instance.Database;

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
    public struct LocalStats
    {
        public int hp;
        public int maxHp;
        public int actionPointsSpeed;
        public int[] res;//0 blunt 1 fire 2 ice 3 magic 4 pierce 5 slash
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

    public LocalStats stats = new();
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

    Bars bars;
    #endregion Declarations

    #region Unity Methods
    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bars = GetComponent<Bars>();
        baseColor = spriteRenderer.color;

        SetInitialState();
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
    public abstract void SetInitialState();
    public void DropItems()
    {
        List<Drop> droppedItems = new();

        foreach(var drop in itemPool)
        {
            int r = Random.Range(0, 101);
            if (r <= drop.dropChance) droppedItems.Add(drop);
        }

        foreach(var drop in droppedItems)
        {
            drop.item.amount += Random.Range(drop.minAmount, drop.maxAmount + 1);
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

    public void CalcDmg(int damage)
    {
        //int finalDamage = 0;

        //foreach(var value in baseStats.resistances.Values())
        //{
        //    finalDamage += ;
        //}
    }
    public void TakeDamage(int damage)
    {
        
        stats.hp -= damage;
        bars.SetHealthBarFillAmount();
        if (stats.hp <= 0) Destroy(gameObject);
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

        Player.target = this;
    }
    #endregion
}
