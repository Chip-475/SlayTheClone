using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class Tester : MonoBehaviour
{
    [SerializeField] private DatabaseSO _database;

    public void Generate(InputAction.CallbackContext context)
    {
        if (!context.performed || _database.skillPrefabs[0] == null) return;

        CombatManager.instance.deck.DrawCards(GameRules.instance.gameRules.nCardsAtTurnStart);
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
