using UnityEngine;

public class KnightAnimTrigger : MonoBehaviour
{
    private EnemyKnight knight => GetComponentInParent<EnemyKnight>();

    public void AnimationTrigger() => knight.AnimationTrigger();

    public void AttackTrigger()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(knight.attackCheck.position, knight.attackCheckRadius);

        foreach (var c in coll)
        {
            if (c.GetComponent<Player>() != null)
            {
                c.GetComponent<Player>().StartFX();
                c.GetComponent<Player>().DamageImpact();
                //Damage at
            }
        }
    }
}
