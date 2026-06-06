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
    private void FixedUpdate()
    {
        if(_actionBarCanMove) actionBarAmount += stats.spdPerSecond;
        if(actionBarAmount >= 100)
        {
            BattleManager.instance.actingEntities.Add(this);
            BattleManager.EntityActing();
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
    public IEnumerator BattleAction()
    {
        yield return null;
    }
    public void StopActionBar()
    {
        _actionBarCanMove = false;
    }
    public void StartActionBar()
    {
        _actionBarCanMove = true;
    }

    //
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
    private void OnEnable()
    {
        BattleManager.OnCombatStart += SetInitialState;
    }
    private void OnDisable()
    {
        BattleManager.OnCombatStart -= SetInitialState;
    }
}
