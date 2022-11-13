using UnityEngine;
using Animator = UnityEngine.Animator;
using Physics = UnityEngine.Physics;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

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
        var moveDirection = stateMachine.transform.rotation * Vector3.forward;

        var inputX = stateMachine.InputReader.GetRawMovement().x;
        
        inputX = Mathf.Abs(inputX) > 0 ? Mathf.Sign(inputX) : 0;
        
        moveDirection = Quaternion.AngleAxis( inputX * 100 * stateMachine.RageControl * Time.deltaTime, Vector3.up) * moveDirection;
        
        
        stateMachine.velocity.x = moveDirection.x * stateMachine.RageSpeed;
        stateMachine.velocity.z = moveDirection.z * stateMachine.RageSpeed;
    }
    
    private void RagedMove()
    {
        stateMachine.Controller.Move(stateMachine.velocity * Time.deltaTime);
    }

    public override void OnCollision(ControllerColliderHit hit)
    {
        if(hit.gameObject.layer == LayerMask.NameToLayer("Ground")) return;

        var obstruction = hit.gameObject.GetComponent<Obstruction>();
        
        if (obstruction)
            hit.gameObject.GetComponent<Obstruction>().HitObstruction();
        
        var vel = stateMachine.velocity;
        vel.y = 0;
        var oldVelocity = vel.magnitude;
        var direction = Vector3.Reflect(vel.normalized, hit.normal);
        direction.y = 0;
        stateMachine.velocity = direction * Mathf.Max(oldVelocity, 0f);
        
        Vector3 faceDirection = new(stateMachine.velocity.x, 0f, stateMachine.velocity.z);

        if (faceDirection == Vector3.zero)
            return;

        stateMachine.transform.rotation = Quaternion.LookRotation(faceDirection);
        
        RagedMove();
    }
}
