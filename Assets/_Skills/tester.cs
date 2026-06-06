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

    public void Debug()
    {
        print("clicked");
    }
}
