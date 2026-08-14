using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSlot : SlotUI
{
    public SkillEntry currentEntry;
    [Space]
    public int id;
    public int loadoutID;
    public bool IsLoadout => loadoutID != 0;
    public bool Occupied => currentEntry != null;

    public override void OnDrop(PointerEventData eventData)
    {
        if (!eventData.pointerDrag.TryGetComponent(out SkillEntry dd)) return;
        if (Occupied) return;

        dd.droppedSuccessfully = true;
        dd.currentSlot = this;

        if (IsLoadout) dd.loadoutID = loadoutID;
        else dd.loadoutID = 0;
    }

    public void CheckState()
    {
        if (currentEntry == null) return;

        if(currentEntry.currentSlot != this) currentEntry = null;
    }
    private void OnEnable()
    {
        LoadoutMenuManager.OnEntryMoved += CheckState;
    }
    private void OnDisable()
    {
        LoadoutMenuManager.OnEntryMoved -= CheckState;
    }
}
