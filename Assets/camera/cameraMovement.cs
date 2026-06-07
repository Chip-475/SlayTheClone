using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScroll : MonoBehaviour
{
    private Vector3 dragOrigin;
    public Transform topleft;
    public Transform bottomright;
    private Mouse mouse;

    void Awake()
    {
        mouse = Mouse.current;
    }

    public void Update()
    {
        if (mouse.leftButton.wasPressedThisFrame)
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
        }

        if (mouse.leftButton.isPressed)
        {
            Vector3 current = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
            Vector3 delta = dragOrigin - current;

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

        float scroll = mouse.scroll.ReadValue().y;
        if (scroll != 0f)
        {
            Camera.main.orthographicSize = Mathf.Clamp(
                Camera.main.orthographicSize - scroll, 0.5f, 4f);
        }
    }
}