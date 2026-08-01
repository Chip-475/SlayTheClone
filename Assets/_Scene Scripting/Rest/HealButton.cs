using UnityEngine;

public class HealButton : MonoBehaviour
{
    #pragma warning disable
    RestManager restManager => RestManager.instance;

    public void Heal()
    {
        PlayerManager.player.Health += 5;

        RestManager.DecreaseTime(3);
    }
}
