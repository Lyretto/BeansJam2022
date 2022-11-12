using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody ridgidBody;
    private PlayerInput input;


    [SerializeField] private float speed = 1;
    [SerializeField] private float rotateSpeed = 1;
    
    private void Awake()
    {
        ridgidBody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        var rawInput = Input.Instance.GetRawMovement()  * speed;

        ridgidBody.velocity = new Vector3(rawInput.x, 0, rawInput.y);

        var currentSpeed = ridgidBody.velocity.sqrMagnitude;
        
        if (currentSpeed < 1f) return;
        var lookPos = ridgidBody.velocity;
        var lookRot = Quaternion.LookRotation(lookPos);
        lookRot.eulerAngles = new Vector3(0, lookRot.eulerAngles.y,0);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotateSpeed * Time.deltaTime);
    }
}
