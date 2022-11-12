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
    }

    public override void Tick()
    {
        CalculateMoveRageDirection();
        FaceMoveDirection();
        RagedMove();

        stateMachine.Animator.SetFloat(moveSpeedHash, new Vector3(stateMachine.velocity.x, 0, stateMachine.velocity.z).sqrMagnitude > 0f ? 1f : 0f, AnimationDampTime, Time.deltaTime);
    }

    public override void Exit()
    { }


    private void CalculateMoveRageDirection()
    {
        Vector3 cameraRight = new(stateMachine.MainCamera.right.x, 0, stateMachine.MainCamera.right.z);

        var moveDirection = cameraRight.normalized * stateMachine.InputReader.GetRawMovement().x;

        // stateMachine.velocity.x = Mathf.Lerp(stateMachine.velocity.x, moveDirection.x * stateMachine.RageSpeed, stateMachine.RageControl);
        // stateMachine.velocity.z = Mathf.Lerp(stateMachine.velocity.z, moveDirection.z * stateMachine.RageSpeed, stateMachine.RageControl);

        var normalizedVector = new Vector3(stateMachine.velocity.x, 0, stateMachine.velocity.z).normalized;

        stateMachine.velocity.x = normalizedVector.x * stateMachine.RageSpeed;
        stateMachine.velocity.z = normalizedVector.y * stateMachine.RageSpeed;
    }
    
    private void RagedMove()
    {
        stateMachine.Controller.Move(stateMachine.velocity * Time.deltaTime);
    }
}
