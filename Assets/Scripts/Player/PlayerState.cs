using UnityEngine;

//Base class for the states of player (e.g MoveState, IdleState, ...)
public class PlayerState
{
    //References to other components
    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected float horInput;
    protected float verInput;

    protected float stateTimer;
    private string animBoolName;

    //Constructor
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        player = _player;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;
    }

    public virtual void EnterState()
    {
        player.anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        horInput = Input.GetAxisRaw("Horizontal");
        verInput = Input.GetAxisRaw("Vertical");
    }

    public virtual void ExitState()
    {
        player.anim.SetBool(animBoolName, false);
    }
}
