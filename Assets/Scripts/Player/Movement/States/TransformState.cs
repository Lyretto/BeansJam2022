using UnityEngine;
using Animator = UnityEngine.Animator;
using Physics = UnityEngine.Physics;

public class TransformState : PlayerBaseState
{
    private readonly int moveSpeedHash = Animator.StringToHash("MoveSpeed");
    private readonly int moveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
    private const float AnimationDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public TransformState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.velocity.y = Physics.gravity.y;
        
        stateMachine.Animator.SetFloat(moveSpeedHash, 0f, AnimationDampTime, Time.deltaTime);

        stateMachine.Animator.CrossFadeInFixedTime(moveBlendTreeHash, CrossFadeDuration);
    }

    public override void Tick()
    { }

    public override void Exit()
    { }
}
