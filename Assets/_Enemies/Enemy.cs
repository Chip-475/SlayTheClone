using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using System.Collections;
using Unity.VisualScripting;

// Enemy base class
public abstract class Enemy : MonoBehaviour, IBattleEntity, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private EnemyStatsSO _baseStats;
    public enum mood
    {
        neutral,
        aggressive,
        defensive,
        desperate,
        coordinated
    }
    public enum battlePlan
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

        public mood currentMood;
        public battlePlan currentBattlePlan;
    }
    public struct LocalStats
    {
        public int hp;
        public int maxHp;
        public int spdPerSecond;
    }

    public LocalStats stats = new();
    public Awareness awareness = new();
    public float actionBarAmount;
    bool _actionBarCanMove;
    public int id;

    public List<SkillSO> skillList = new();
    [Space]

    public SortingGroup group;
    public Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Color baseColor;
    protected bool selected;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        group = GetComponent<SortingGroup>();
        baseColor = spriteRenderer.color;
    }
    protected virtual void FixedUpdate()
    {
        if(_actionBarCanMove) actionBarAmount += stats.spdPerSecond;
        if(actionBarAmount >= 100)
        {
            TurnManager.instance.actingEntities.Add(this);
        }
    }

    //
    void SetInitialState()
    {
        // Clone stats from asset to local class to avoid modifying all enemies
        stats.hp = _baseStats.hp;
        stats.maxHp = _baseStats.maxHp;
        stats.spdPerSecond = _baseStats.spdPerSecond;

        // Preps for combat
        actionBarAmount = 0f;
        _actionBarCanMove = true;
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
        _actionBarCanMove = true;
    }

    public abstract IEnumerator BattleAction();
    public void TakeDamage(int damage)
    {
        stats.hp -= damage;
        if (stats.hp <= 0) Destroy(gameObject);
    }

    // Pointer Events
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        animator.SetTrigger("Hurt");
    }

    // Management
    protected virtual void OnEnable()
    {
        BattleManager.OnCombatStart += SetInitialState;
    }
    protected virtual void OnDisable()
    {
        BattleManager.OnCombatStart -= SetInitialState;
    }
}
