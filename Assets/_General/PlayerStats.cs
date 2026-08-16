using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public int hp;
    public int maxHp;
    public int atk;
    public int actionPointsSpeed;

    [Header("Misc")]
    public int timesDied;
    public int enemiesKilled;
    public int distanceTravelled;
    public int moneyEarned;
    public int cardsPlayed;
    public int totDamageDealt;
    public int highestDamageDealt;

    [Tooltip("ID of the enemy that killed the player.")]
    public string killer;
}