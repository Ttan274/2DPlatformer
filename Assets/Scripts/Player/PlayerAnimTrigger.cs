using UnityEngine;

public class PlayerAnimTrigger : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    public void ShootTrigger()
    {
        player.playerAttack.ShootFireball();
    }

    public void AttackTrigger()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var c in coll)
        {
            if(c.GetComponent<Enemy>() != null)
            {
                c.GetComponent<Enemy>().DamageBehaviour(player.damage);
            }
        }
    }
}
