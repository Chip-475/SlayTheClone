using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class LoadoutSlot : MonoBehaviour, IDropHandler
{
    public abstract Type AcceptedType { get; }

    public virtual void OnDrop(PointerEventData eventData)
    {
        Component component = eventData.pointerDrag.GetComponent(AcceptedType);
        DraggableItemUI item = component as DraggableItemUI;

        if (item == null) return;

        item.transform.SetParent(transform);
        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        item.MarkDropped();
    }
}
