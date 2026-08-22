using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDrag : MonoBehaviour
{
    private PlayerInput input;

    [SerializeField] private float dragSpeed = 0.03f;
    [SerializeField] private BoxCollider2D mapBounds;

    private Camera cam;

    private void Awake()
    {
        input = new PlayerInput();
        cam = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        input.Camera.Enable();
    }

    private void OnDisable()
    {
        input.Camera.Disable();
    }

    private void Update()
    {
        if (input.Camera.Drag.IsPressed() &&
            MapManager.menuHistory.Count == 0)
        {
            Vector2 delta = input.Camera.PointerDelta.ReadValue<Vector2>();

            transform.position -=
                new Vector3(delta.x, delta.y, 0) * dragSpeed;
        }

        ClampCamera();
    }

    private void ClampCamera()
    {
        Bounds bounds = mapBounds.bounds;

        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        float minX = bounds.min.x + halfWidth;
        float maxX = bounds.max.x - halfWidth;
        float minY = bounds.min.y + halfHeight;
        float maxY = bounds.max.y - halfHeight;

        float x = minX > maxX
            ? bounds.center.x
            : Mathf.Clamp(transform.position.x, minX, maxX);

        float y = minY > maxY
            ? bounds.center.y
            : Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector3(x, y, transform.position.z);
    }
}