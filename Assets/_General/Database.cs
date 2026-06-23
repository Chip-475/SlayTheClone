using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("Database"), menuName = ("Database"))]
public class Database : ScriptableObject
{
    // ONLY CREATE A SINGLE INSTANCE

    [Header("Game Rules")]
    public int maxCardsInHand = 5;
    public int startingCardsCount;
    public int cardsPerTurn;

    [Header("Game Data")]
    public List<SkillCard> skillPrefabs = new();
    public List<Enemy> enemyPrefabs = new();
}
