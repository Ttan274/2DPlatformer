using UnityEngine;

public class KnightAnimTrigger : MonoBehaviour
{
    private EnemyKnight knight => GetComponentInParent<EnemyKnight>();

    public void AnimationTrigger() => knight.AnimationTrigger();
}
