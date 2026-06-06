using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScroll : MonoBehaviour
{
    private Vector3 dragOrigin;
    public Transform topleft;
    public Transform bottomright;
    Mouse mouse = Mouse.current;
    public void Update()
    {
        if (mouse.leftButton.isPressed)
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
            Vector3 delta = dragOrigin - Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
            if (transform.position.x + delta.x < topleft.position.x)
                delta.x = topleft.position.x - transform.position.x;
            if (transform.position.x + delta.x > bottomright.position.x)
                delta.x = bottomright.position.x - transform.position.x;
            if (transform.position.y + delta.y > topleft.position.y)
                delta.y = topleft.position.y - transform.position.y;
            if (transform.position.y + delta.y < bottomright.position.y)
                delta.y = bottomright.position.y - transform.position.y;
            transform.position += delta;
        }
        if(mouse.IsActuated())
        {
            float scroll = mouse.scroll.ReadValue().y;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - scroll, 2f, 20f);
        }
    }
}
