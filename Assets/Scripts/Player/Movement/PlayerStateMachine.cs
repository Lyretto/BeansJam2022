using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerStateMachine : StateMachine
{
    public Vector3 velocity;
    public float MovementSpeed = 5f;

    public float RageControl = 0.1f;

    public float RageSpeed = 20f;


    public Transform chooseDirectionObject;
    public float ChooseDirectionTime = 3f;
    public float ChooseDirectionSpeed = 3f;
    public float LookRotationDampFactor  => 10f;
    public Transform MainCamera { get; private set; }
    public Input InputReader { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }

    private void Start()
    {
        if (Camera.main is not null) MainCamera = Camera.main.transform;

        InputReader = Input.Instance;
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();

        SwitchState(new PlayerMoveState(this));
    }
    private void OnEnable()
    {
        GameEvents.Instance.rage.AddListener(() => SwitchState(new ShootingMoveState(this)));
        GameEvents.Instance.transforming.AddListener((_) => SwitchState(new TransformState(this)));
        GameEvents.Instance.calm.AddListener(() => SwitchState(new PlayerMoveState(this)));
        GameEvents.Instance.interactInput.AddListener(InterruptShootingMoveState);
    }

    private void OnDisable()
    {
        if (!GameEvents.Instance) return;
        GameEvents.Instance.rage.RemoveListener(() => SwitchState(new ShootingMoveState(this)));
        GameEvents.Instance.transforming.RemoveListener((_) => SwitchState(new TransformState(this)));
        GameEvents.Instance.calm.RemoveListener(() => SwitchState(new PlayerMoveState(this)));
    }

    private void InterruptShootingMoveState()
    {
        if(CurrentState.GetType() == typeof(ShootingMoveState))
            SwitchState(new DemonMoveState(this));
    }
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        CurrentState.OnCollision(hit);
    }
}