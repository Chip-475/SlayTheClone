using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class Tester : MonoBehaviour
{
    [SerializeField] private Database _database;

    public void Generate(InputAction.CallbackContext context)
    {
        if(!context.performed || _database.skillPrefabs[0] == null) return;

        HandManager.instance.AddCard(_database.skillPrefabs[Random.Range(0, _database.skillPrefabs.Count)]);
    }

    public void TestClick(InputAction.CallbackContext context)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            print($"Hovering {hit.collider.gameObject.name}");
        }
    }
}
