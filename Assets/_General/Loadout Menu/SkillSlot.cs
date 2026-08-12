using UnityEngine;
using UnityEngine.EventSystems;
using static Database;

public class SkillSlot : SlotUI, IDropHandler
{
    public int id;
    
    public new void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);

        var entry = eventData.pointerDrag.GetComponent<SkillEntry>();
        entry.id = id;
    }
}
