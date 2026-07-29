using UnityEngine;

public class HealButton : MonoBehaviour
{
    #pragma warning disable
    RestManager restManager => RestManager.instance;

    public void Heal()
    {
        PlayerManager.instance.stats.hp += 5;

        RestManager.DecreaseTime(3);
    }
}
