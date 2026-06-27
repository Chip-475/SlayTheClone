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

        CombatManager.instance.deck.DrawCards(CombatManager.instance.Database.nCardsAtTurnStart);
    }
}
