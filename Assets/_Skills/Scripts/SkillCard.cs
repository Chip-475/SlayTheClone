using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using static UnityEngine.RuleTile.TilingRuleOutput;
using DG.Tweening;

// Attached to card game object
public class SkillCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public GameObject self;
    public _Skill skill;
    [Space]
    public SpriteRenderer background;
    public SpriteRenderer image;
    [Space]
    public TMP_Text cost;
    public TMP_Text effect;
    public TMP_Text desc;
    [Space]
    public SortingGroup group;

    private bool isHoveredOn = false;
    private Vector3 basePos;

    private void Start()
    {
        cost.text = $"{skill.cost}";
        effect.text = $"{skill.atkMin} - {skill.atkMax}";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isHoveredOn) return;
        
        basePos = transform.position;
        transform.DOMove(basePos + new Vector3(0, 1, 0), 0.15f);
        group.sortingOrder = 99;
        isHoveredOn = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isHoveredOn) return;

        transform.DOMove(basePos, 0.15f);
        group.sortingOrder = 0;
        isHoveredOn = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isHoveredOn || BattleManager.instance.actions.Count >= BattleManager.instance.numberOfActions) return;

        if(GameManager.State != GameManager.GameState.Selecting) GameManager.State = GameManager.GameState.Selecting;
        BattleManager.instance.currentSelected = this;
    }
}
