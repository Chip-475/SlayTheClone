using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class tester : MonoBehaviour
{
    public void generate(InputAction.CallbackContext context)
    {
        if(!context.performed || Database.instance.skillPrefabs[0] == null) { return; }

        HandManager.instance.AddCard(Database.instance.skillPrefabs[0]);
    }

    public void debug()
    {
        print("clicked");
    }
}
