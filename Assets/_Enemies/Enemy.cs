using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

// Enemy base class
public abstract class Enemy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public EnemyStats stats;
    public List<Skill> skillList = new List<Skill>();

    protected SpriteRenderer spriteRenderer;
    protected Color baseColor;
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
        spriteRenderer.color = baseColor;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
