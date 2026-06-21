using UnityEngine;

public class Globals : MonoBehaviour
{
    public static Globals instance;

    public Database db;

    private void Awake()
    {
        instance = this;
    }
}
