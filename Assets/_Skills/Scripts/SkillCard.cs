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
    public SpriteRenderer wrapperRenderer;
    public PlayerStatsSO stats;

    private bool isHoveredOn = false;
    public Vector3 basePos;
    public Quaternion baseRot;

    private void Awake()
    {
        wrapperRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        cost.text = $"{skill.cost}";
        effect.text = $"{skill.atkMin} - {skill.atkMax}";
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isHoveredOn) return;
        
        transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1, 0), 0.15f);
        wrapperRenderer.sortingOrder = 99;
        isHoveredOn = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isHoveredOn) return;

        transform.DOMove(new Vector3(transform.position.x, transform.position.y - 1, 0), 0.15f);
        wrapperRenderer.sortingOrder = 0;
        isHoveredOn = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isHoveredOn) return;

        Player.cardInUse = this;
        Player.selecting = true;
        print("Card Selected");
    }
}
