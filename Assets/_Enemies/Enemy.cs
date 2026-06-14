using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using System.Collections;
using Unity.VisualScripting;

// Enemy base class
public abstract class Enemy : MonoBehaviour, IBattleEntity, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] protected EnemyStatsSO baseStats;
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
    public struct LocalStats
    {
        public int hp;
        public int maxHp;
        public int actionPointsSpeed;
        public int[] res;//0 blunt 1 fire 2 ice 3 magic 4 pierce 5 slash
    }

    public LocalStats stats = new();
    public Awareness awareness = new();
    public float actionPoints;
    public bool canGainActionPoints;
    public int id;

    public List<SkillSO> skillList = new();
    [Space]

    public SortingGroup group;
    public Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Color baseColor;
    protected bool selected;

    public Bars bars;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        group = GetComponent<SortingGroup>();
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
        if(actionPoints >= 100 && !TurnManager.instance.actingEntities.Contains(this))
        {
            TurnManager.instance.actingEntities.Add(this);
        }
    }

    //
    public abstract void SetInitialState();

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

    public abstract IEnumerator BattleAction();

    public void CalcDmg(int damage, List<DamageTypeSO> types)
    {
        float minRes = float.MaxValue;
        foreach (var t in types)
        {
            //0 blunt 1 fire 2 ice 3 magic 4 pierce 5 slash
            if (t.name == "blunt" && stats.res[0] <= minRes)
            {
                minRes = stats.res[0];
            }
            if (t.name == "fire" && stats.res[1] <= minRes)
            {
                minRes = stats.res[1];
            }
            if (t.name == "ice" && stats.res[2] <= minRes)
            {
                minRes = stats.res[2];
            }
            if (t.name == "magic" && stats.res[3] <= minRes)
            {
                minRes = stats.res[3];
            }
            if (t.name == "pierce" && stats.res[4] <= minRes)
            {
                minRes = stats.res[4];
            }
            if (t.name == "slash" && stats.res[5] <= minRes)
            {
                minRes = stats.res[4];
            }
        }
        TakeDamage((int)(damage*minRes));
    }
    public void TakeDamage(int damage)
    {
        
        stats.hp -= (int)damage;
        bars.SetHealthBarFillAmount();
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
        if (!Player.selecting) return;

        Player.target = this;
        print("Target Selected");
    }

    // Management
    public abstract void OnEnable();
    public abstract void OnDisable();
}
