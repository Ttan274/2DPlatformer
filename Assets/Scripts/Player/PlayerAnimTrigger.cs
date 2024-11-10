using UnityEngine;

public class PlayerAnimTrigger : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    public void AttackTrigger()
    {
        player.playerAttack.ShootFireball();
    }
}
