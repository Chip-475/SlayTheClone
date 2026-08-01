using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using Unity.Mathematics;
using System;
using System.Linq;
using static EnemyInfoSO;
using System.Threading.Tasks;

// Enemy base class
[RequireComponent(typeof(EnemyBars))]
public abstract class Enemy : MonoBehaviour, IBattleEntity, IPointerDownHandler
{
    #region Declarations
    [SerializeField] protected EnemyInfoSO baseInfo;

    public EnemyInfoSO info;

    public float actionPoints;
    public bool canGainActionPoints;
    public int id;
    public int ID { get { return id; } set { id = value; } }

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
        CombatManager.instance.enemiesOnField.Add(this);
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
            CombatManager.instance.StartCoroutine(CombatManager.PerformActions());
        }
    }

    public abstract void OnEnable();
    public abstract void OnDisable();
    #endregion

    #region Methods
    public virtual void Init()
    {
        // Preps for combat
        actionPoints = 0f;
        canGainActionPoints = true;
    }
    public virtual void DropItems()
    {
        if (info.dropPool.Count == 0) return;

        var drop = info.dropPool[UnityEngine.Random.Range(0, info.dropPool.Count)];
        int amountToDrop = UnityEngine.Random.Range(drop.minAmount, drop.maxAmount);

        CombatManager.instance.results.drops.Add(drop.item, amountToDrop);
    }
    public virtual void SwitchAnimation(string animName)
    {
        animator.CrossFade(animName, 0, 0);
    }
    public virtual AnimationClip GetAnimation(string animName)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animName)
            {
                return clip;
            }
        }

        print($"No clip named {animName} found.");
        return null;
    }
    public void ActionBarMovement(bool active)
    {
        if (active) { canGainActionPoints = true; }
        else { canGainActionPoints = false; }
    }

    public virtual IEnumerator Action() { yield break; }
    public virtual int ApplyResistances(int damage)
    {
        // damage calculation
        // preferrably use dictionary for resistance storing

        return damage;
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
        DropItems();
        CombatManager.instance.enemiesOnField.Remove(this);

        Destroy(gameObject);
        yield break;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CombatManager.instance.selectedSkill == null) return;

        CombatManager.instance.selectedEnemy = this;
    }
    #endregion Methods
}
