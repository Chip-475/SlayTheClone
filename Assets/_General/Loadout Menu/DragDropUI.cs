using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropUI : MonoBehaviour, IBeginDragHandler, IDragHandler ,IEndDragHandler
{
    public Transform originalParent;
    public Vector3 originalPosition;
    public bool droppedSuccessfully;

    public Canvas canvas;
    public RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    public void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = rectTransform.parent;
        canvasGroup.blocksRaycasts = false;
        originalPosition = rectTransform.anchoredPosition;

        transform.SetParent(canvas.transform);
        droppedSuccessfully = false;
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (droppedSuccessfully) return;

        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = originalPosition;
    }
}
