using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

// Enemy base class
public abstract class Enemy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public EnemyStats stats;
    public List<_Skill> skillList = new();
    public SpriteRenderer spriteRenderer;
    public SortingGroup group;

    protected Color baseColor;
    protected bool selected;
    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseColor = spriteRenderer.color;
    }

    public void OnAttacked()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        spriteRenderer.color = Color.black;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(selected) return;

        spriteRenderer.color = baseColor;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(GameManager.State != GameManager.GameState.Selecting) return;

        selected = true;
        if (!BattleManager.instance.currentTargets.Contains(this)) BattleManager.instance.currentTargets.Add(this);
        if(BattleManager.instance.currentTargets.Count == BattleManager.instance.currentSelected.skill.numberOfTargets) BattleManager.instance.BuildPlayerAction();
    }
}
