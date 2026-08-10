using UnityEngine;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour, IDropHandler
{
    RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(!eventData.pointerDrag.TryGetComponent(out DragDropUI dd)) return;

        dd.droppedSuccessfully = true;
        dd.rectTransform.anchoredPosition = rectTransform.anchoredPosition;
    }
}
