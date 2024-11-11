using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public EnemyState currentState {  get; private set; }

    public void Initialize(EnemyState enemyState)
    {
        currentState = enemyState;
        currentState.EnterState();
    }

    public void ChangeState(EnemyState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
