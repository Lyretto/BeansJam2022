using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;

    protected void SwitchState(State state)
    {
        currentState?.Exit();
        currentState = state;
        currentState.Enter();
    }

    private void Update()
    {
        currentState?.Tick();
    }
}