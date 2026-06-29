using UnityEngine;

public class HealButton : MonoBehaviour
{
    #pragma warning disable

    RestManager restManager => RestManager.instance;

    public void Heal()
    {
        restManager.Database.playerStats.hp += 5;

        // Time decrease if needed
    }
}
