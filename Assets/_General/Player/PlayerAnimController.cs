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
        if (player.state == Player.State.Idle) player.animator.CrossFade("Idle", 0, 0);
        if (player.state == Player.State.LightAttacking) player.animator.CrossFade("LightAttack", 0, 0);
        if (player.state == Player.State.HeavyAttacking) player.animator.CrossFade("HeavyAttack", 0, 0);
        if (player.state == Player.State.Casting) player.animator.CrossFade("Cast", 0, 0);
        if (player.state == Player.State.Dead) player.animator.CrossFade("Death", 0, 0);
    }
}
