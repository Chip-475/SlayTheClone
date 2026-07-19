using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {
        if (player.state == Player.State.Idle) player.animator.CrossFade("Player Idle", 0, 0);
        if (player.state == Player.State.LightAttacking) player.animator.CrossFade("Player Light Attack", 0, 0);
        if (player.state == Player.State.HeavyAttacking) player.animator.CrossFade("Player Heavy Attack", 0, 0);
        if (player.state == Player.State.Casting) player.animator.CrossFade("Player Cast", 0, 0);
        if (player.state == Player.State.Dead) player.animator.CrossFade("Player Death", 0, 0);
    }
}
