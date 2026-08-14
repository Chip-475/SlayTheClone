using UnityEngine;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour, IDropHandler
{
    public virtual void OnDrop(PointerEventData eventData)
    {
        if(!eventData.pointerDrag.TryGetComponent(out DragDropUI dd)) return;

        dd.droppedSuccessfully = true;
        dd.transform.SetParent(transform);
        dd.rectTransform.anchoredPosition = Vector3.zero;
    }
}
