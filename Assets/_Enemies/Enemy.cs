using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using System.Collections;

// Enemy base class
public abstract class Enemy : MonoBehaviour, IBattleEntity, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private EnemyStatsSO _baseStats;
    public struct LocalStats
    {
        public int hp;
        public int maxHp;
        public int spdPerSecond;
    }

    public LocalStats stats = new();
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
