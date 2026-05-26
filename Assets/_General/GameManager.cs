using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        None,
        Selecting,
        Paused
    }
    public static GameState State;
}
