using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerStateMachine : StateMachine
{
    public Vector3 velocity;
    public float MovementSpeed => 5f;
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
}