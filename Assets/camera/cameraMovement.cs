using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScroll : MonoBehaviour
{
    private Vector3 dragOrigin;

    void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;

        if (mouse.leftButton.wasPressedThisFrame)
            dragOrigin = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());

        if (mouse.leftButton.isPressed)
        {
            Vector3 delta = dragOrigin - Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
            transform.position += delta;
        }
        if (mouse.scroll.ReadValue().y != 0)
        {
            float scrollAmount = mouse.scroll.ReadValue().y * 0.1f;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - scrollAmount, 2f, 20f);
        }
    }
}
