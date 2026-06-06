using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class move : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    bool dragging;
    public void OnPointerDown(PointerEventData eventData)
    {
        dragging = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
    }
    void Update()
    {
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
        }
    }
}
