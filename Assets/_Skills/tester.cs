using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class tester : MonoBehaviour
{
    public void generate(InputAction.CallbackContext context)
    {
        if(!context.performed || Database.instance.skillPrefabs[0] == null) { return; }

        HandManager.instance.AddCard(Database.instance.skillPrefabs[Random.Range(0, Database.instance.skillPrefabs.Count)]);
    }

    public void debug()
    {
        print("clicked");
    }
}
