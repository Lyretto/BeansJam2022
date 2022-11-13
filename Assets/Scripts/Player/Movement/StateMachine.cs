using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State CurrentState;

    protected void SwitchState(State state)
    {
        CurrentState?.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }

    private void Update()
    {
        CurrentState?.Tick();
    }
}