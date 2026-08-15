using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillEntry : DragDropUI
{
    Image image;

    public SkillSlot currentSlot;
    public Skill skill;
    [Space]
    public int id;
    public int loadoutID;
    public bool IsEquipped => loadoutID != 0;


    private new void Awake()
    {
        base.Awake();

        image = GetComponent<Image>();
    }
    private void Start()
    {
        skill = Database.GetSkillById(id);
        image.sprite = skill.data.icon;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = rectTransform.parent;
        canvasGroup.blocksRaycasts = false;
        originalPosition = rectTransform.anchoredPosition;

        transform.SetParent(canvas.transform);
        droppedSuccessfully = false;

        if (IsEquipped) currentSlot = LoadoutMenuManager.instance.loadoutSlots[loadoutID - 1];
        else currentSlot = LoadoutMenuManager.instance.slots[id];
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(currentSlot.transform);
        rectTransform.anchoredPosition = Vector2.zero;

        LoadoutMenuManager.EntryMoved();
    }
}
