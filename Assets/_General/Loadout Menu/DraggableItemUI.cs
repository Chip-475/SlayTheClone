using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItemUI : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    Canvas canvas;
    CanvasGroup canvasGroup;
    RectTransform rectTransform;
    
    [SerializeField] Transform originalParent;
    Vector2 originalPosition;

    public bool droppedSuccessfully;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        originalParent = rectTransform.parent;
        originalPosition = rectTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        canvasGroup.blocksRaycasts = true;

        if (droppedSuccessfully) return;

        transform.SetParent(originalParent);
        rectTransform.position = originalPosition;
    }
    public void MarkDropped() { droppedSuccessfully = true; }
}
