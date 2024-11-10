using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        player.playerAttack.ChangeDotVisibility(true);
    }

    public override void ExitState()
    {
        base.ExitState();
        player.playerAttack.ChangeDotVisibility(false);
        player.StartCoroutine("BusyRoutine", 0.2f);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(0, 0);

        if (Input.GetKeyUp(KeyCode.Mouse1))
            stateMachine.ChangeState(player.idleState);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (player.transform.position.x > mousePos.x && player.facingDir == 1)
            player.Flip();
        else if (player.transform.position.x < mousePos.x && player.facingDir == -1)
            player.Flip();
    }
}
