using UnityEngine;
using Animator = UnityEngine.Animator;
using Physics = UnityEngine.Physics;

public class ShootingMoveState : PlayerBaseState
{
    private readonly int moveSpeedHash = Animator.StringToHash("MoveSpeed");
    private readonly int moveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
    private const float AnimationDampTime = 0.1f;
    private float timer;
    private Quaternion currentRotation;
    

    public ShootingMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.velocity.y = Physics.gravity.y;
        
        stateMachine.Animator.SetFloat(moveSpeedHash, 0f);

        stateMachine.chooseDirectionObject.gameObject.SetActive(true);

        timer = stateMachine.ChooseDirectionTime;
        currentRotation = stateMachine.chooseDirectionObject.rotation;
    }

    public override void Tick()
    {
        timer -= Time.deltaTime;
        
        currentRotation = Quaternion.AngleAxis( stateMachine.ChooseDirectionSpeed * timer, Vector3.up);

        stateMachine.chooseDirectionObject.rotation = currentRotation;
        
        if (timer <= 0)
        {
            stateMachine.SwitchState(new DemonMoveState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.transform.rotation = currentRotation;
        stateMachine.chooseDirectionObject.gameObject.SetActive(false);
        stateMachine.velocity.x = Vector3.forward.x * stateMachine.RageSpeed;
        stateMachine.velocity.z = Vector3.forward.z * stateMachine.RageSpeed;
        Move();
        stateMachine.Animator.SetFloat(moveSpeedHash, 1f);
    }
}
