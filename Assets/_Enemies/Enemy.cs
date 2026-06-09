using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using System.Collections;

// Enemy base class
public abstract class Enemy : MonoBehaviour, IBattleEntity, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public EnemyStatsSO _baseStats;
    public struct LocalStats
    {
        public int hp;
        public int maxHp;
        public int spdPerSecond;
    }

    public LocalStats stats = new();
    public bool canGainActionPoints;
    public float actionPoints;
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

        SetInitialState();
    }
    protected virtual void FixedUpdate()
    {
        if(canGainActionPoints) actionPoints += stats.spdPerSecond * Time.deltaTime;
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
    public abstract void OnEnable();
    public abstract void OnDisable();
}
