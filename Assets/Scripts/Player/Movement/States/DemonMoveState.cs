using UnityEngine;
using Animator = UnityEngine.Animator;
using Physics = UnityEngine.Physics;

public class DemonMoveState : PlayerBaseState
{
    private readonly int moveSpeedHash = Animator.StringToHash("MoveSpeed");
    private readonly int moveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
    private const float AnimationDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public DemonMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.velocity.y = Physics.gravity.y;

        stateMachine.Animator.CrossFadeInFixedTime(moveBlendTreeHash, CrossFadeDuration);
    }

    public override void Tick()
    {
        CalculateMoveDirection();
        FaceMoveDirection();
        Move();

        stateMachine.Animator.SetFloat(moveSpeedHash, stateMachine.InputReader.GetRawMovement().sqrMagnitude > 0f ? 1f : 0f, AnimationDampTime, Time.deltaTime);
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}
