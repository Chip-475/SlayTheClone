using UnityEngine;

public class CoRoutineRunner : MonoBehaviour
{
    public static CoRoutineRunner instance;

    private void Start()
    {
        instance = this;
    }
}
