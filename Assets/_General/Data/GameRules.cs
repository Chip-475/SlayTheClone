using UnityEngine;

public class GameRules : MonoBehaviour
{
    public static GameRules instance;

    public GameRulesSO gameRules;

    private void Awake()
    {
        instance = this;
    }
}
