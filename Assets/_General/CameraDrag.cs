using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable
public class CameraDrag : MonoBehaviour
{
    PlayerInput input;

    [SerializeField]
    float dragSpeed = 0.02f;

    private void Awake()
    {
        input = new();
    }

    private void OnEnable()
    {
        input.Camera.Enable();
    }

    private void OnDisable()
    {
        input.Camera.Disable();
    }

    void Update()
    {
        if (!input.Camera.Drag.IsPressed() || MapManager.instance.menuHistory.Count > 0) return;

        Vector2 delta = input.Camera.PointerDelta.ReadValue<Vector2>();
        transform.position -= new Vector3(delta.x, delta.y, 0) * dragSpeed;
    }
}