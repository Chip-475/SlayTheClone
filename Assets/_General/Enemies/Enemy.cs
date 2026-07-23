using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using Unity.Mathematics;
using System;
using System.Linq;

// Enemy base class
[RequireComponent(typeof(EnemyBars))]
public abstract class Enemy : MonoBehaviour, IBattleEntity, IPointerDownHandler
{
    #region Declarations
    [SerializeField] protected EnemyInfoSO baseInfo;

    [HideInInspector] public EnemyInfoSO info;

    public float actionPoints;
    public bool canGainActionPoints;
    public int id;

    public Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Color baseColor;
    protected bool selected;

    EnemyBars bars;
    #endregion Declarations

    #region Unity Methods
    public virtual void Awake()
    {
        info = Instantiate(baseInfo);
    }
    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bars = GetComponent<EnemyBars>();
        animator = GetComponent<Animator>();

        transform.localScale *= 0.8f;
    }
    protected virtual void FixedUpdate()
    {
        if (canGainActionPoints)
        {
            actionPoints += info.speed * Time.deltaTime;
        }
        if(actionPoints >= 100 && !CombatManager.instance.actingEntities.Contains(this))
        {
            CombatManager.instance.actingEntities.Add(this);
            CombatManager.PerformActions();
        }
    }

    public abstract void OnEnable();
    public abstract void OnDisable();
    #endregion

    #region Methods
    public virtual void SetInitialState()
    {
        // Preps for combat
        actionPoints = 0f;
        canGainActionPoints = true;
    }
    public virtual void DropItems()
    {
        List<EnemyInfoSO.Drop> droppedItems = new();

        foreach(var drop in info.dropPool)
        {
            int rand = UnityEngine.Random.Range(0, 101);
            if (rand <= drop.dropChance) droppedItems.Add(drop);
        }

        foreach(var drop in droppedItems)
        {
            int amountToDrop = UnityEngine.Random.Range(drop.minAmount, drop.maxAmount);
            ItemDatabase.instance.itemTable[drop.item.id].amount += amountToDrop;
        }
    }
    public virtual void SwitchAnimation(string animName)
    {
        animator.CrossFade(animName, 0, 0);
    }

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

    public virtual IEnumerator Action() { yield break; }

    public virtual int ApplyResistances(int damage)
    {
        float finalDamage = 0;

        var damageValues = CombatManager.instance.selectedCard.skill.damageTable.Values().ToList();
        var resistanceValues = baseInfo.resistances.Values().ToList();
        

        for (int i = 0; i < damageValues.Count; i++)
        {
            finalDamage += damageValues[i] * resistanceValues[i];
        }

        return (int)Math.Ceiling(finalDamage);
    }
    public virtual void TakeDamage(int damage)
    {
        int finalDamage = ApplyResistances(damage);
        info.hp -= finalDamage;

        bars.SetHealthBarFillAmount();

        if (info.hp <= 0)
        {
            CombatManager.instance.StartCoroutine(DeathSequence());
        }
    }
    public virtual IEnumerator DeathSequence()
    {
        // basically OnDeath event.
        // death anim, game stats increases etc...
        DropItems();

        Destroy(gameObject);
        yield break;
    }

    #endregion Methods

    #region Events
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!Player.selecting) return;

        CombatManager.instance.selectedEnemy = this;

        CombatManager.instance.executor.Execute();
    }
    #endregion
}

#region Pending
//public enum Mood
//{
//    Neutral,
//    Aggressive,
//    Defensive,
//    Desperate,
//    Doordinated
//}
//public enum BattlePlan
//{
//    attack,
//    heal,
//    buff,
//    debuff,
//    defend
//}
//[System.Serializable]
//public struct Awareness
//{
//    [Header("hp")]
//    public int currentHP;
//    public int maxHP;

//    [Header("playerStats")]
//    public int playerHP;
//    public int playerMaxHP;
//    public int playerMoney;

//    [Header("Allies")]
//    public int aliveAllies;
//    public int totalAllies;
//    public bool isAnHealerAlly;
//    public bool isASupporterAlly;
//    public bool isADPSAlly;
//    public bool isATankAlly;

//    [Header("type")]
//    public bool isBoss;
//    public bool isElite;
//    public bool isEnemy;

//    [Header("killInfo")]
//    public bool canKillPlayer;
//    public bool canBeKilled;
//    public bool AlliesCanBeKilled;

//    [Header("actions")]
//    public bool canHeal;
//    public bool canBuff;
//    public bool canDebuffPlayer;
//    public bool canSummon;

//    [Header("dmgInfo")]
//    public int avgPlayerDamage;
//    public int avgDmgToPlayer;
//    public int totalPersonalDmgToPlayer;

//    [Header("lastTurnInfo")]
//    public bool wasAttackedLastTurn;
//    public bool wasAllyKilledLastTurn;
//    public bool wasHealedLastTurn;
//    public bool didPlayerHealLastTurn;
//    public bool didPlayerBuffLastTurn;

//    public Mood currentMood;
//    public BattlePlan currentBattlePlan;
//}
#endregion
