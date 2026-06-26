using UnityEngine;

[CreateAssetMenu(fileName = "Game Rules", menuName = "Scriptable Objects/Game Rules")]
public class GameRulesSO : ScriptableObject
{
    public int nStartingCards;
    public int nCardsAtTurnStart;
}
