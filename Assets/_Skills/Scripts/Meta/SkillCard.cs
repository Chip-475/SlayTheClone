using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using DG.Tweening;

// Attached to card game object
public class SkillCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public GameObject self;
    public SkillSO skill;
    [Space]
    public SpriteRenderer background;
    public SpriteRenderer image;
    [Space]
    public TMP_Text cost;
    public TMP_Text effect;
    public TMP_Text desc;
    [Space]
    public SortingGroup group;
    public PlayerStatsSO stats;

    private bool isHoveredOn = false;
    public Vector3 basePos;
    public Quaternion baseRot;

    private void Start()
    {
        cost.text = $"{skill.cost}";
        effect.text = $"{skill.atkMin} - {skill.atkMax}";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isHoveredOn) return;
        
        transform.DOMove(new Vector3(transform.position.x, -2, 0), 0.15f);
        transform.DORotate(new Vector3(0, 0, 0), 0.15f);
        group.sortingOrder = 99;
        isHoveredOn = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isHoveredOn) return;

        transform.DOMove(basePos, 0.15f);
        transform.DORotate(baseRot.eulerAngles, 0.15f);
        group.sortingOrder = 0;
        isHoveredOn = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isHoveredOn) return;
        if(Player.instance.stamina < skill.cost)
        {
            print("Insufficient stamina to use card.");
            return;
        }

        Player.cardInUse = this;
        Player.selecting = true;
        print("Card Selected");
    }
}
