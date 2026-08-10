using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropUI : MonoBehaviour, IBeginDragHandler, IDragHandler ,IEndDragHandler
{
    public Vector3 originalPosition;
    public bool droppedSuccessfully;

    [SerializeField] Canvas canvas;
    public RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        originalPosition = rectTransform.anchoredPosition;

        droppedSuccessfully = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if(!droppedSuccessfully) { rectTransform.anchoredPosition = originalPosition; } 
    }
}
