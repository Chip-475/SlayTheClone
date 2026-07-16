using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using DG.Tweening;

// Attached to card game object
public class SkillCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    MainDatabase Database => MainDatabase.instance;

    public int id;

    public SkillSO skill;
    [Space]
    public TMP_Text costText;
    public TMP_Text descText;
    [Space]
    public SortingGroup wrapperGroup;
    public MainDatabase.PlayerStats Stats => Database.playerStats;

    private bool isHoveredOn = false;
    public Vector3 basePos;
    public Quaternion baseRot;

    private void Awake()
    {
        wrapperGroup = GetComponent<SortingGroup>();
    }
    private void Start()
    {
        costText.text = skill.cost.ToString();
        descText.text = $"Strike an enemy with moderate force, dealing <color=orange>{skill.atkMin} - {skill.atkMax}</color> damage."; 
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isHoveredOn) return;
        
        transform.DOMove(new Vector3(basePos.x, basePos.y + 1, 0), 0.15f);
        wrapperGroup.sortingOrder = 99;
        isHoveredOn = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isHoveredOn) return;

        transform.DOMove(new Vector3(basePos.x, basePos.y, 0), 0.15f);
        wrapperGroup.sortingOrder = 1;
        isHoveredOn = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isHoveredOn) return;

        CombatManager.instance.selectedCard = this;
        Player.selecting = true;
    }
}
