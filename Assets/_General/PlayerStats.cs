using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public int hp = 25;
    public int maxHp = 25;
    public int atk = 3;
    public int actionPointsSpeed = 60;

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